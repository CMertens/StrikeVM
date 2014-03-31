using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public enum VmStateTypes {
        NO_SUCH_STATE,
        NOT_READY,
        READY,
        PROCESSING,
        SLEEPING,       // Sleeping due to tick count
        ABORTED,        // Error raised in execution
        HALTED,         // Halted by OpCode
        KILLED,         // Exceeded time or memory constraints
        DEBUG,           // Debug mode by OpCode
        END_OF_EXECUTION
    };

    public partial class VirtualMachine {

        // Environment turnstile, used for serialization purposes.
        internal int numberOfEnvironmentsCreated = 0;

        // Current operating environment
        internal Environment CurrentEnvironment;

        // List of all generated environments for serialization purposes.
        internal List<Environment> Environments;

        // Saved outer environments for when a closure is called.
        internal List<Environment> ClosureStack;

        // program text
        internal CodeMemory ByteCode;

        // list of return addresses used for code block and closure calls
        internal List<int> ReturnAddresses;

        // When processing text, if we encounter a code block or closure start,
        // we keep state during its definition in this list.
        internal CodeBlock CodeBlockBeingDefined;
        internal int CodeBlocksAsText = 0;

        internal long Ticks = 0;
        internal long KillTickLimit = -1;
        internal long SleepTickLimit = -1;
        internal VmStateTypes State = VmStateTypes.NO_SUCH_STATE;

        // Used to bind and define primitive calls
        internal Dictionary<String, Action<VirtualMachine, Instruction, List<Value>>> Primitives;

        // Used to track argument arity
        internal Dictionary<String, List<Value>> PrimitivesArity;

        // Global-scope variables (used for integrating Strike in with documents)
        internal Dictionary<String, Value> Globals;

        // VM version
        internal double version = 1.0;

        // What feature key-values this vm supports
        internal Dictionary<String, List<String>> Features;

        // What feature key-values the bytecode currently requires
        internal Dictionary<String, List<String>> Requirements;

        // Any errors raised
        internal List<String> Errors;

        public Stack<Value> Stack {
            get {
                return CurrentEnvironment.Stack;
            }
        }

        public int ProgramCounter {
            get {
                return this.ByteCode.ProgramCounter;
            }
        }

        internal bool ContinueExecution = false;
        public string DebugText = "";

        public bool CanBeRestarted {
            get {
                return (
                    State == VmStateTypes.SLEEPING ||
                    State == VmStateTypes.DEBUG    ||
                    State == VmStateTypes.KILLED
                );
            }
        }

        public bool IsSleeping {
            get {
                return State == VmStateTypes.SLEEPING;
            }
        }

        public bool IsDebugging {
            get {
                return State == VmStateTypes.DEBUG;
            }
        }

        public bool IsKilled {
            get {
                return State == VmStateTypes.KILLED;
            }
        }

        public bool IsAborted {
            get {
                return State == VmStateTypes.ABORTED;
            }
        }

        public bool IsHalted {
            get {
                return State == VmStateTypes.HALTED;
            }
        }

        public bool IsDone {
            get {
                return State == VmStateTypes.END_OF_EXECUTION;
            }
        }

        public VirtualMachine() {            
            ReturnAddresses = new List<int>();
            CurrentEnvironment = new Environment();
            ByteCode = new CodeMemory();
            Errors = new List<string>();
            State = VmStateTypes.NOT_READY;

            Features = new Dictionary<string, List<string>>();
            Requirements = new Dictionary<string, List<string>>();
            Globals = new Dictionary<string, Value>();
            Primitives = new Dictionary<string, Action<VirtualMachine, Instruction, List<Value>>>();
            PrimitivesArity = new Dictionary<string, List<Value>>();
            Environments = new List<Environment>();
            ClosureStack = new List<Environment>();
            Environments.Add(CurrentEnvironment);
        }

        public VirtualMachine(IEnumerable<Instruction> ins) {
            ReturnAddresses = new List<int>();
            CurrentEnvironment = new Environment();
            ByteCode = new CodeMemory(ins);
            Errors = new List<string>();
            State = VmStateTypes.READY;

            Features = new Dictionary<string, List<string>>();
            Requirements = new Dictionary<string, List<string>>();
            Globals = new Dictionary<string, Value>();
            Primitives = new Dictionary<string, Action<VirtualMachine, Instruction, List<Value>>>();
            PrimitivesArity = new Dictionary<string, List<Value>>();

            Environments = new List<Environment>();
            ClosureStack = new List<Environment>();
            Environments.Add(CurrentEnvironment);
        }

        public void AddPrimitive(String name, Action<VirtualMachine, Instruction, List<Value>> primitive, List<Value> argTypes){
            this.Primitives.Add(name, primitive);
            this.PrimitivesArity.Add(name, argTypes);
        }

        public void Start() {
            CurrentEnvironment = new Environment();
            ByteCode.BindLabels();
            Run();
        }

        public void Collect() {
            // Stop-the-world GC for picking up unused environments and prototypes
            List<int> envs = new List<int>();   // Track down closed environments that no longer have codeblock closures
            Dictionary<int, List<int>> prots = new Dictionary<int, List<int>>();    // Look for prototypes without objectreferences

        }

        public void Run() {
            ContinueExecution = true;
            while (ByteCode.IsValidInstruction && ContinueExecution) {
                State = VmStateTypes.PROCESSING;
                OpCodes.Process(this, ByteCode.CurrentInstruction);                
                if (this.Errors.Count() > 0) {
                    State = VmStateTypes.ABORTED;                    
                    break;
                }
                if (State == VmStateTypes.DEBUG) {
                    break;
                }
                if (this.KillTickLimit > 0 && this.Ticks >= this.KillTickLimit) {
                    this.RaiseError("Processing time limits exceeded.");
                    State = VmStateTypes.KILLED;
                    break;
                }
                if (this.SleepTickLimit > 0 && (this.Ticks % this.SleepTickLimit) == 0) {
                    State = VmStateTypes.SLEEPING;
                    break;
                }
                State = VmStateTypes.END_OF_EXECUTION;
            }

        }

        

        public void SetReturn(int i) {
            ReturnAddresses.Add(i);
        }

        public void Return() {
            int ret = ReturnAddresses[ReturnAddresses.Count() - 1];
            ReturnAddresses.RemoveAt(ReturnAddresses.Count() - 1);
            ByteCode.ProgramCounter = ret;
        }

        public void RaiseError(String error) {
            Errors.Add(error);
            ContinueExecution = false;
        }

        public void RaiseDebug(String reason) {
            State = VmStateTypes.DEBUG;
            DebugText = reason;
        }

        public void RaiseHalt(String reason) {
            State = VmStateTypes.HALTED;
            DebugText = reason;
        }

        /// <summary>
        /// Creates an environment, saves it for serialization.
        /// </summary>
        /// <returns></returns>
        public Environment PushClosure() {
            Environment env = new Environment();
            numberOfEnvironmentsCreated++;
            env.EnvironmentID = numberOfEnvironmentsCreated;
            Environments.Add(env);
            return env;
        }

        public void PushEnvironment() {
            Environment env = new Environment();
            numberOfEnvironmentsCreated++;
            env.EnvironmentID = numberOfEnvironmentsCreated;
            env.Parent = CurrentEnvironment;
            CurrentEnvironment = env;
            Environments.Add(env);
        }

        public void PopEnvironment() {
            Environment env = CurrentEnvironment;
            CurrentEnvironment = env.Parent;
            Environments.Remove(env);
        }


        internal bool Exists(String name) {
            if (CurrentEnvironment.Variables.ContainsKey(name)) {
                return true;
            }
            return false;
        }

        internal void Let(String name, ValueTypes valueType) {
            CurrentEnvironment.Variables.Add(name, Value.New(valueType));
        }

        internal void Set(String name, Value val) {
            CurrentEnvironment.Variables[name] = val;
        }

        internal Value Get(String name) {
            return CurrentEnvironment.Variables[name];
        }


        internal void SetGlobal(String name, Value val) {
            Globals.Add(name, val);
        }

        internal Value GetGlobal(String name) {
            return Globals[name];
        }

        internal bool GlobalsExists(String name) {
            return Globals.ContainsKey(name);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM
{
    public partial class OpCodes {

        public static void Process(VirtualMachine vm, Instruction i) {

            
            if (vm.CodeBlockBeingDefined != null) { // We're scanning this opcode as the text of a codeblock, not for execution
                // We've encountered a close command without having processing a start command as text. We want to process
                // this as an opcode.
                if (vm.CodeBlocksAsText == 0 && (i.Type == OpCodeTypes.END_BLOCK || i.Type == OpCodeTypes.END_CLOSURE || i.Type == OpCodeTypes.END_FUNCTION)) {
                    // Just making the logic clear; do nothing and fall through to the switch monster
                } else {
                    // Increment the number of codeblocks we've encountered as text
                    if (i.Type == OpCodeTypes.START_BLOCK || i.Type == OpCodeTypes.START_CLOSURE || i.Type == OpCodeTypes.START_FUNCTION) {
                        vm.CodeBlocksAsText++;
                    }
                    // Decrement the number of encountered codeblocks
                    if ((i.Type == OpCodeTypes.END_BLOCK || i.Type == OpCodeTypes.END_CLOSURE || i.Type == OpCodeTypes.END_FUNCTION) && vm.CodeBlocksAsText > 0) {
                        vm.CodeBlocksAsText--;
                    }
                    // The opcode being processed should not be executed; it's inside a 
                    // definition region for a code block or closure.
                    vm.ByteCode.ProgramCounter++;
                    return;
                }
            }
            switch (i.Type) {
                case OpCodeTypes.DEBUG:
                    OpCodes.Debug(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.SWAP:
                    OpCodes.Swap(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                // I guess every well-behaved program should end with a HALT, but we shouldn't depend on that.
                case OpCodeTypes.HALT:
                    OpCodes.Halt(vm,i);
                    break;

                case OpCodeTypes.ABORT:
                    OpCodes.Abort(vm, i);
                    break;

                // Guaranteed to run in Log(10) time. At least as far as the VM is concerned.
                case OpCodeTypes.LABEL:
                case OpCodeTypes.NOP:                    
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.ADD:
                    OpCodes.Add(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.SUBTRACT:
                    OpCodes.Subtract(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.MULTIPLY:
                    OpCodes.Multiply(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.DIVIDE:
                    OpCodes.Divide(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.MODULO:
                    OpCodes.Modulo(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;


                case OpCodeTypes.EQ:
                    OpCodes.TestEquals(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.NEQ:
                    OpCodes.TestNotEquals(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.LT:
                    OpCodes.TestLessThan(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.LTE:
                    OpCodes.TestLessThanEquals(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GT:
                    OpCodes.TestGreaterThan(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GTE:
                    OpCodes.TestGreaterThanEquals(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.TYPE_EQ:
                    OpCodes.CheckTypeEquality(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;
                
                case OpCodeTypes.NUMERIC_EQ:
                    OpCodes.CheckTypeNumeracy(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.IS_TYPE:
                    OpCodes.IsType(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.PUSH:
                    OpCodes.Push(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    break;

                case OpCodeTypes.POP:
                    OpCodes.Pop(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.EXISTS:
                    OpCodes.VarExists(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GET_LOCAL:
                    OpCodes.GetLocal(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GET:
                    OpCodes.Get(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.TYPESET_NEAR:
                case OpCodeTypes.SET_NEAR:
                    OpCodes.SetNear(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GET_TOP:
                    OpCodes.GetGlobal(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.TYPESET_TOP:
                case OpCodeTypes.SET_TOP:
                    OpCodes.SetGlobal(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.SET:
                case OpCodeTypes.TYPESET:
                    OpCodes.Set(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.SET_VAR:
                case OpCodeTypes.TYPESET_VAR:
                    OpCodes.SetVar(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GET_VAR:
                    OpCodes.GetVar(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

/***************************
                case OpCodeTypes.TYPELET:
                case OpCodeTypes.LET:
                    OpCodes.Let(vm,i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    vm.CurrentEnvironment.Memory++;
                    break;
***************************/

                case OpCodeTypes.DUPE:
                    OpCodes.Dupe(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

/***********************
                case OpCodeTypes.TYPELETSET:
                case OpCodeTypes.LETSET :
                    OpCodes.LetSet(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    vm.CurrentEnvironment.Memory++;
                    break;
***********************/
                case OpCodeTypes.NEW:
                    OpCodes.CreatePrototype(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.CLONE:
                    OpCodes.Clone(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.COPY:
                    OpCodes.Copy(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.TYPESETPROP:
                case OpCodeTypes.SETPROP:
                    OpCodes.SetPrototypeProperty(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GETPROP:
                    OpCodes.GetPrototypeProperty(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.DELETEPROP:
                    OpCodes.DeletePrototypeProperty(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.LENGTH:
                    OpCodes.Length(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.RETURN_BLOCK:
                    OpCodes.Return(vm, i);
                    break;

                case OpCodeTypes.RETURN_CLOSURE:
                    OpCodes.ReturnClosure(vm, i);
                    break;

                case OpCodeTypes.RETURN_FUNCTION:
                    OpCodes.ReturnFunction(vm, i);
                    break;

                case OpCodeTypes.SET_RETURN_ABSOLUTE:
                case OpCodeTypes.SET_RETURN_RELATIVE:
                    OpCodes.SetReturn(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.SET_RETURN_LABEL:
                    OpCodes.SetLabelReturn(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.START_CLOSURE:
                case OpCodeTypes.START_BLOCK:
                case OpCodeTypes.START_FUNCTION:
                    OpCodes.StartBlock(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.END_BLOCK:
                case OpCodeTypes.END_CLOSURE:
                case OpCodeTypes.END_FUNCTION:
                    OpCodes.EndBlock(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;
               
                case OpCodeTypes.CALL_CLOSURE:
                case OpCodeTypes.CALL_BLOCK:
                case OpCodeTypes.CALL_FUNCTION:
                    OpCodes.CallFunction(vm, i);
                    vm.Ticks++;
                    break;

                case OpCodeTypes.CALL_PRIMITIVE:
                    OpCodes.CallPrimitive(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.TAIL_CALL:
                    OpCodes.CallTailCall(vm, i);                    
                    vm.Ticks++;
                    break;

                case OpCodeTypes.JUMP_ABSOLUTE:
                case OpCodeTypes.JUMP_RELATIVE:
                    OpCodes.Jump(vm, i);
                    vm.Ticks++;
                    break;

                case OpCodeTypes.JUMP_LABEL:
                    OpCodes.JumpLabel(vm, i);
                    vm.Ticks++;
                    break;

                case OpCodeTypes.T_JUMP_ABSOLUTE:
                case OpCodeTypes.T_JUMP_RELATIVE:
                    OpCodes.TrueJump(vm, i);
                    vm.Ticks++;
                    break;

                case OpCodeTypes.T_JUMP_LABEL:
                    OpCodes.TrueJumpLabel(vm, i);
                    vm.Ticks++;
                    break;

                case OpCodeTypes.F_JUMP_ABSOLUTE:
                case OpCodeTypes.F_JUMP_RELATIVE:
                    OpCodes.FalseJump(vm, i);
                    vm.Ticks++;
                    break;

                case OpCodeTypes.F_JUMP_LABEL:
                    OpCodes.FalseJumpLabel(vm, i);
                    vm.Ticks++;
                    break;

                case OpCodeTypes.APPEND_RANGE:
                case OpCodeTypes.APPEND:
                    OpCodes.Append(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.ARRAY_GET:
                    OpCodes.ArrayGet(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.ARRAY_SET:
                    OpCodes.ArraySet(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.STRING_SPLICE:
                    OpCodes.SpliceString(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.STRING_APPEND:
                    OpCodes.AppendString(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.STACK_SIZE:
                    OpCodes.StackSize(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.CLOCK_UTC_TICKS:
                    OpCodes.ClockUtcTicks(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.MAKE_REF:
                    OpCodes.MakeReference(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.GET_REF:
                    OpCodes.GetReference(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.TYPESET_REF:
                case OpCodeTypes.SET_REF:
                    OpCodes.SetReference(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                case OpCodeTypes.DELETE:
                    OpCodes.Delete(vm, i);
                    vm.ByteCode.ProgramCounter++;
                    vm.Ticks++;
                    break;

                default:
                    vm.RaiseError("Unknown opcode " + i.Type);
                    break;
            }
        }

        /// <summary>
        /// Takes a string off the stack and creates a reference to the variable of the same name.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void MakeReference(VirtualMachine vm, Instruction i) {
            Value name = vm.Stack.Shift();
            if (name.Type != ValueTypes.STRING || name.String_Value == null || name.String_Value == "") {
                vm.RaiseError("Tried to reference a bound value with a non-string name");
                return;
            }
            if (vm.CurrentEnvironment.Exists(name.String_Value) == false) {
                vm.RaiseError("Tried to create a reference to non-existent value name " + name.String_Value);
                return;
            }
            Reference refc = new Reference();
            refc.HomeEnvironment = vm.CurrentEnvironment;
            refc.Name = name.String_Value;
            Value val = Value.New(ValueTypes.REFERENCE, refc);
            vm.Stack.Push(val); 
        }

        /// <summary>
        /// Pushes the current UTC time in ticks onto the stack.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void ClockUtcTicks(VirtualMachine vm, Instruction i) {
            Value v = Value.New(ValueTypes.UINT_64, DateTime.UtcNow.Ticks);
            vm.Stack.Push(v);
        }

        internal static void FalseJump(VirtualMachine vm, Instruction i) {
            Value a = vm.Stack.Shift();
            if (a.Type != ValueTypes.BOOLEAN) {
                vm.RaiseError("Attempted a test jump condition on a non-boolean value");
                return;
            }
            if (a.Boolean_Value == false) {
                OpCodes.Jump(vm, i);
                return;
            }
            vm.ByteCode.ProgramCounter++;
            return;
        }

        internal static void FalseJumpLabel(VirtualMachine vm, Instruction i) {
            Value a = vm.Stack.Shift();
            if (a.Type != ValueTypes.BOOLEAN) {
                vm.RaiseError("Attempted a test jump condition on a non-boolean value");
                return;
            }
            if (a.Boolean_Value == false) {
                OpCodes.JumpLabel(vm, i);
                return;
            }
            vm.ByteCode.ProgramCounter++;
            return;
        }

        internal static void TrueJump(VirtualMachine vm, Instruction i) {
            Value a = vm.Stack.Shift();
            if (a.Type != ValueTypes.BOOLEAN) {
                vm.RaiseError("Attempted a test jump condition on a non-boolean value");
                return;
            }
            if (a.Boolean_Value == true) {
                OpCodes.Jump(vm, i);
                return;
            }
            vm.ByteCode.ProgramCounter++;
            return;
        }

        internal static void TrueJumpLabel(VirtualMachine vm, Instruction i) {
            Value a = vm.Stack.Shift();
            if (a.Type != ValueTypes.BOOLEAN) {
                vm.RaiseError("Attempted a test jump condition on a non-boolean value");
                return;
            }
            if (a.Boolean_Value == true) {
                OpCodes.JumpLabel(vm, i);
                return;
            }
            vm.ByteCode.ProgramCounter++;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Jump(VirtualMachine vm, Instruction i) {
            if (i.Args.Count() < 1) {
                vm.RaiseError("Attempted to jump without destination");
                return;
            }
            if (i.Args[0].Type != ValueTypes.INT_32) {
                vm.RaiseError("Attempted to jump to non-Int32 destination");
                return;
            }
            if (i.Type == OpCodeTypes.JUMP_ABSOLUTE || i.Type == OpCodeTypes.T_JUMP_ABSOLUTE || i.Type == OpCodeTypes.F_JUMP_ABSOLUTE) {
                vm.ByteCode.ProgramCounter = i.Args[0].Int32_Value;
            } else if (i.Type == OpCodeTypes.JUMP_RELATIVE || i.Type == OpCodeTypes.F_JUMP_RELATIVE || i.Type == OpCodeTypes.T_JUMP_RELATIVE) {
                vm.ByteCode.ProgramCounter += i.Args[0].Int32_Value;
            } else {
                vm.RaiseError("Unknown jump opcode " + i.Type);
            }
        }

        internal static void JumpLabel(VirtualMachine vm, Instruction i) {
            if (i.Args.Count() < 1) {
                vm.RaiseError("Attempted to label jump without destination");
                return;
            }
            if (i.Args[0].Type != ValueTypes.STRING) {
                vm.RaiseError("Attempted to label jump to non-string destination");
                return;
            }
            if (vm.ByteCode.Labels.ContainsKey(i.Args[0].String_Value) == false) {
                vm.RaiseError("Attempted to label jump to nonexistent label " + i.Args[0].String_Value);
                return;
            }
            vm.ByteCode.ProgramCounter = vm.ByteCode.Labels[i.Args[0].String_Value];
        }

        /// <summary>
        /// 
        /// 
        /// The difference
        /// between RETURN_BLOCK and RETURN_CLOSURE *is* meaningful, as the former will attempt to return context
        /// to parent scope and the latter will attempt to return context to saved scope. Mixing up the two will
        /// cause memory corruption and probable execution failure.
        /// 
        /// Note that we push arguments for a call from 0...n (end), and recovered the same way, as we shift them and 
        /// move them to the new stack, where they will be removed in LIFO order.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void CallFunction(VirtualMachine vm, Instruction i) {
            Value callable = vm.Stack.Shift();
            if (callable.Type != ValueTypes.CODE_BLOCK) {
                vm.RaiseError("Tried to call a non-callable value as a function");
                return;
            }

            if (i.Type == OpCodeTypes.CALL_CLOSURE && callable.CodeBlock_Value.Closure == null) {
                vm.RaiseError("Tried to call a non-closure block with lexical scope.");
                return;
            }

            if ( (i.Type == OpCodeTypes.CALL_BLOCK || i.Type == OpCodeTypes.CALL_FUNCTION) && callable.CodeBlock_Value.Closure != null) {
                vm.RaiseError("Tried to call a closure with dynamic scope.");
                return;
            }

            // Create a new environment and shift arguments onto the stack
            Environment env = null;
            if (i.Type == OpCodeTypes.CALL_BLOCK) {             // dynamically-scoped
                env = vm.PushCurrentEnvironment();
            } else if (i.Type == OpCodeTypes.CALL_FUNCTION) {   // non-closure lexical scope (function)
                env = vm.PushClosure();
            } else if (i.Type == OpCodeTypes.CALL_CLOSURE) {    // lexically-scoped (closure)
                env = callable.CodeBlock_Value.Closure;
            } else {
                vm.RaiseError("Attempt to call block or closure with unknown opcode " + i.Type);
                return;
            }

            
            for (int aInc = callable.CodeBlock_Value.ArgumentsArity.Count() - 1; aInc >= 0; aInc--) {
                Value v = callable.CodeBlock_Value.ArgumentsArity[aInc];
                Value arg = vm.Stack.Shift();
                if (v.Type != ValueTypes.ANY_TYPE && arg.Type != v.Type) {
                    vm.RaiseError("Argument arity error: expected type " + arg.Type + ", saw " + v.Type);
                    return;
                }
                env.Stack.Push(arg);
            }

            // If we're operating in lexical scope, save the current environment so we can return
            // back to it.
            if (i.Type == OpCodeTypes.CALL_CLOSURE || i.Type == OpCodeTypes.CALL_FUNCTION) {
                vm.ClosureStack.Add(vm.CurrentEnvironment);
            }

            // Finally, add the code block to the VM's function call list. We only use this to
            // enable tail calls so we don't lose tex start locations and function arity
            vm.FunctionCallList.Add(callable.CodeBlock_Value);

            vm.CurrentEnvironment = env;
            vm.ByteCode.ProgramCounter = callable.CodeBlock_Value.StartProgramCounter;
        }


        /// <summary>
        /// When TAIL_CALL is invoked, the VM inspects the top CodeBlock in the FunctionCallList,
        /// pops the argument off the local stack, clears the local stack, clears local arguments,
        /// pushes the arguments back onto the stack, and then moves the VM PC to the top of the
        /// current CodeBlock. This allows for stack-free(ish) recursion.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void CallTailCall(VirtualMachine vm, Instruction i) {
            if (vm.FunctionCallList.Count() < 1) {
                vm.RaiseError("Attempt to invoke tail call outside of a code block.");
                return;
            }
            CodeBlock cb = vm.FunctionCallList.Last();
            List<Value> argStack = new List<Value>();
            for (int aInc = cb.ArgumentsArity.Count() - 1; aInc >= 0; aInc--) {
                Value v = cb.ArgumentsArity[aInc];
                Value arg = vm.Stack.Shift();
                if (v.Type != ValueTypes.ANY_TYPE && arg.Type != v.Type) {
                    vm.RaiseError("Argument arity error: expected type " + arg.Type + ", saw " + v.Type);
                    return;
                }
                argStack.Add(arg);
            }
            vm.CurrentEnvironment.Stack.Clear();
            vm.CurrentEnvironment.Variables.Clear();
            vm.CurrentEnvironment.Memory = 0;
            vm.CurrentEnvironment.Prototypes.Clear();
            vm.CurrentEnvironment.Objects.Clear();
            foreach (Value v in argStack) {
                vm.Stack.Push(v);
            }
            vm.ByteCode.ProgramCounter = cb.StartProgramCounter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void CallPrimitive(VirtualMachine vm, Instruction i) {
            Value pName = vm.Stack.Shift();
            if (pName.Type != ValueTypes.STRING) {
                vm.RaiseError("Tried to call a primitive with a non-string name");
                return;
            }
            Action<VirtualMachine, Instruction, List<Value>> prim = null;
            if (vm.Primitives.ContainsKey(pName.String_Value) == false) {
                vm.RaiseError("Tried to call empty primitive " + pName.String_Value);
                return;
            }
            prim = vm.Primitives[pName.String_Value];

            Value[] arity = new Value[vm.PrimitivesArity[pName.String_Value].Count()];

            vm.PrimitivesArity[pName.String_Value].CopyTo(arity);

            // We are going to get arguments in reverse order
            for (int x = arity.Count() - 1; x >= 0; x--) {
                Value v = vm.Stack.Shift();
                if (arity[x].Type != ValueTypes.ANY_TYPE && (arity[x].Type != v.Type)) {
                    vm.RaiseError("Argument arity error for primitive " + pName.String_Value + ": expected type " + arity[x].Type + ", saw " + v.Type);
                    return;
                }
                arity[x] = v;
            }
            prim(vm, i, new List<Value>(arity));
            return;
        }


        /// <summary>
        /// Starts a code block definition by pushing a new block into the vm for further definition
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void StartBlock(VirtualMachine vm, Instruction i) {
            if (vm.CodeBlockBeingDefined != null) {
                throw new Exception("VM Error: Code block already being defined.");
            }
            CodeBlock call = new CodeBlock();
            foreach (Value v in i.Args) {
                call.ArgumentsArity.Add(v);
            }
            int inc = vm.ByteCode.ProgramCounter + 1;
            call.StartProgramCounter = inc;

            if (i.Type == OpCodeTypes.START_CLOSURE) {
                Environment env = vm.PushClosure();
                call.Closure = env;
            }

            vm.CodeBlockBeingDefined = call;
        }

        internal static void EndBlock(VirtualMachine vm, Instruction i) {            
            CodeBlock call = vm.CodeBlockBeingDefined;

            if (i.Type == OpCodeTypes.END_CLOSURE && call.Closure == null) {
                vm.RaiseError("Tried to end a closure with a null environment.");
                return;
            } else if ( (i.Type == OpCodeTypes.END_BLOCK || i.Type == OpCodeTypes.END_FUNCTION) && call.Closure != null) {
                vm.RaiseError("Tried to end a code block or function with a closure.");
                return;
            }
            call.EndProgramCounter = vm.ByteCode.ProgramCounter-1;
            Value block = Value.New(ValueTypes.CODE_BLOCK, call);
            vm.CodeBlockBeingDefined = null;
            vm.Stack.Push(block);
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Dupe(VirtualMachine vm, Instruction i) {
            Value v = vm.Stack.Peek();
            vm.Stack.Push(v.Dupe());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void PushEnvironment(VirtualMachine vm, Instruction i) {
            Environment env = new Environment();
            env.Parent = vm.CurrentEnvironment;
            vm.CurrentEnvironment = env;
        }

        /// <summary>
        /// Changes the execution context to the current environment's parent, and then jumps to the 
        /// address on the ReturnAddress stack. In other words, this explicitly closes dynamic scope.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Return(VirtualMachine vm, Instruction i) {            
            if (vm.CurrentEnvironment.Parent == null) {
                // We're at the top of the chain, so end execution.
                vm.RaiseError("Attempted to return without any parent environment.");
                return;
            }
            if (vm.Stack.Count() > 0) {
                Value val = vm.Stack.Shift();
                vm.CurrentEnvironment.Parent.Stack.Push(val);
            }
            vm.PopCurrentEnvironment();
            vm.ByteCode.ProgramCounter = vm.ReturnAddresses[vm.ReturnAddresses.Count() - 1];
            vm.ReturnAddresses.RemoveAt(vm.ReturnAddresses.Count() - 1);
            vm.FunctionCallList.RemoveAt(vm.FunctionCallList.Count() - 1);
        }

        /// <summary>
        /// This operates like a return, but instead of returning execution context to the parent of the 
        /// current environment, it returns to the environment at the end of the VM's closure stack. In 
        /// other words, this explicitly closes lexical scope.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void ReturnClosure(VirtualMachine vm, Instruction i) {
            
            if (vm.ClosureStack.Count() < 1) {
                vm.RaiseError("Attempted to return a closure without any outer context.");
                return;
            }
            Environment enviro = vm.ClosureStack.Last();
            vm.ClosureStack.RemoveAt(vm.ClosureStack.Count() - 1);

            if (vm.Stack.Count() > 0) {
                Value val = vm.Stack.Shift();
                enviro.Stack.Push(val);
            }
            vm.CurrentEnvironment = enviro;
            vm.ByteCode.ProgramCounter = vm.ReturnAddresses[vm.ReturnAddresses.Count() - 1];
            vm.ReturnAddresses.RemoveAt(vm.ReturnAddresses.Count() - 1);
            vm.FunctionCallList.RemoveAt(vm.FunctionCallList.Count() - 1);
            return;
        }

        /// <summary>
        /// This is similar to the way we handle closures, except functions don't have durable
        /// state, so we destroy the environment after using.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void ReturnFunction(VirtualMachine vm, Instruction i) {

            if (vm.ClosureStack.Count() < 1) {
                vm.RaiseError("Attempted to return a function without any outer context.");
                return;
            }
            Environment temporaryEnv = vm.CurrentEnvironment;
            Environment enviro = vm.ClosureStack.Last();
            vm.ClosureStack.RemoveAt(vm.ClosureStack.Count() - 1);

            if (vm.Stack.Count() > 0) {
                Value val = vm.Stack.Shift();
                enviro.Stack.Push(val);
            }
            vm.CurrentEnvironment = enviro;
            vm.ByteCode.ProgramCounter = vm.ReturnAddresses[vm.ReturnAddresses.Count() - 1];
            vm.ReturnAddresses.RemoveAt(vm.ReturnAddresses.Count() - 1);
            vm.FunctionCallList.RemoveAt(vm.FunctionCallList.Count() - 1);

            vm.DestroyClosure(temporaryEnv);

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void SetReturn(VirtualMachine vm, Instruction i) {
            if (i.Args.Count() < 1 || i.Args[0].Type != ValueTypes.INT_32) {
                vm.RaiseError("Tried to set a return without an offset");
                return;
            }
            int shift = i.Args[0].Int32_Value;
            if (i.Type == OpCodeTypes.SET_RETURN_RELATIVE) {
                vm.ReturnAddresses.Add(vm.ByteCode.ProgramCounter + shift);
                return;
            } else if (i.Type == OpCodeTypes.SET_RETURN_ABSOLUTE) {
                vm.ReturnAddresses.Add(shift);
                return;
            } else {
                vm.RaiseError("Tried to set return with unknown opcode " + i.Type);
                return;
            }
        }

        internal static void SetLabelReturn(VirtualMachine vm, Instruction i) {
            if (i.Args.Count() < 1 || i.Args[0].Type != ValueTypes.STRING || i.Args[0].String_Value == null || i.Args[0].String_Value == "") {
                vm.RaiseError("Tried to set a return without an offset label");
                return;
            }
            if (vm.ByteCode.Labels.ContainsKey(i.Args[0].String_Value) == false) {
                vm.RaiseError("Tried to set return value for nonexistent label " + i.Args[0].String_Value);
            }
            int shift = vm.ByteCode.Labels[i.Args[0].String_Value];
            vm.ReturnAddresses.Add(shift);
            return;
        }

        /// <summary>
        /// Create an object and return a reference.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void CreatePrototype(VirtualMachine vm, Instruction i) {
            Prototype p = new Prototype();
            p.Parent = null;
            p.ParentIndex = -1;
            Value v = vm.CurrentEnvironment.AddObject(p);
            vm.Stack.Push(v);
        }

        /// <summary>
        /// Will return true if the types are the same, or if either type is ANY_TYPE.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void CheckTypeEquality(VirtualMachine vm, Instruction i) {
            Value a = vm.Stack.Shift();
            Value b = vm.Stack.Shift();
            vm.Stack.Push(Value.New(ValueTypes.BOOLEAN, (a.Type == b.Type || a.Type == ValueTypes.ANY_TYPE || b.Type == ValueTypes.ANY_TYPE)));
        }

        internal static void CheckTypeNumeracy(VirtualMachine vm, Instruction i) {
            Value a = vm.Stack.Shift();
            Value b = vm.Stack.Shift();
            vm.Stack.Push(Value.New(ValueTypes.BOOLEAN, (a.IsNumeric() && b.IsNumeric())));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void IsType(VirtualMachine vm, Instruction i) {
            Value a = vm.Stack.Shift();

            if (i.Args.Count() < 1) {
                vm.RaiseError("Attempted to static compare types with no arguments");
                return;
            }
            if (a.Type != i.Args[0].Type && (a.Type != ValueTypes.ANY_TYPE || i.Args[0].Type != ValueTypes.ANY_TYPE)) {
                vm.Stack.Push(Value.New(ValueTypes.BOOLEAN, false));
                return;
            }
            vm.Stack.Push(Value.New(ValueTypes.BOOLEAN, true));
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Length(VirtualMachine vm, Instruction i) {
            Value vValue = vm.Stack.Shift();
            if (vValue.Type != ValueTypes.ARRAY && vValue.Type != ValueTypes.STRING) {
                vm.RaiseError("Tried to get length of a non-array and non-string value " + vValue.Type);
                return;
            }
            Value len = new Value();
            len.Type = ValueTypes.INT_32;
            if (vValue.Type == ValueTypes.ARRAY) {
                len.Int32_Value = vValue.Array_Value.Count();
            } else if (vValue.Type == ValueTypes.STRING) {
                len.Int32_Value = vValue.String_Value.Count();
            }
            vm.Stack.Push(len);
        }

        /// <summary>
        /// ref,prop (end) | ref (end)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void GetPrototypeProperty(VirtualMachine vm, Instruction i) {            
            Value propName = vm.Stack.Shift();
            Value refc = vm.Stack.Shift();

            if (propName.Type != ValueTypes.STRING || propName.String_Value == null || propName.String_Value == "") {
                vm.RaiseError("Tried to access a property without a string value");
                return;
            }
            if (refc.Type != ValueTypes.OBJECT) {
                vm.RaiseError("Tried to access a property on something other than an object reference.");
                return;
            }

            Value val = OpCodes.ReturnProperty(vm, refc, propName);
            vm.Stack.Push(val);
        }

        /// <summary>
        /// value,ref,prop (end) | ref (end)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void SetPrototypeProperty(VirtualMachine vm, Instruction i) {                        
            Value propName = vm.Stack.Shift();
            Value refc = vm.Stack.Shift();
            Value val = vm.Stack.Shift();
            if (propName.Type != ValueTypes.STRING || propName.String_Value == null || propName.String_Value == "") {
                vm.RaiseError("Tried to set a property without a string value");
            }
            if (refc.Type != ValueTypes.OBJECT) {
                vm.RaiseError("Tried to set a property on something other than an object reference.");
            }
            if (i.Type == OpCodeTypes.TYPESETPROP) {
                try {
                    Value existVal = OpCodes.ReturnProperty(vm, refc, propName);
                    if (existVal.Type != val.Type) {
                        vm.RaiseError("Tried to redefine type for property " + propName + ": property is " + existVal.Type + ", new value is " + val.Type);
                    }
                } catch (Exceptions.StrikePropertyNotFoundException) {
                    // ignore
                } catch (Exception e) {
                    vm.RaiseError(e.Message);
                    return;
                }
            }

            ObjectReference reference = refc.ObjectReference_Value;
            reference.Home.SetObjectProperty(reference.Index, propName.String_Value, val);            
            vm.Stack.Push(refc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void DeletePrototypeProperty(VirtualMachine vm, Instruction i) {
            Value propName = vm.Stack.Shift();
            Value refc = vm.Stack.Shift();

            if (propName.Type != ValueTypes.STRING || propName.String_Value == null || propName.String_Value == "") {
                vm.RaiseError("Tried to delete a property without a string value");
            }
            if (refc.Type != ValueTypes.OBJECT) {
                vm.RaiseError("Tried to delete a property on something other than an object reference.");
            }
            
            ObjectReference reference = refc.ObjectReference_Value;

            Prototype p = reference.Home.Prototypes[reference.Home.Objects[reference.Index]];
            while (p != null) {
                if (p.ContainsKey(propName.String_Value)) {
                    p.Remove(propName.String_Value);
                } else {
                    if (p.Parent == null) {
                        p = null;
                    } else {
                        p = p.Parent.Prototypes[p.Parent.Objects[p.ParentIndex]];
                    }
                }
            }
            if (p == null) {
                vm.RaiseError("Tried to remove nonexistent property " + propName.String_Value);
                return;
            }

            vm.Stack.Push(refc);
        }

        internal static Value ReturnProperty(VirtualMachine vm, Value refc, Value propName) {
            Value val = Value.New(ValueTypes.NULL);
            if (propName.Type != ValueTypes.STRING || propName.String_Value == null || propName.String_Value == "") {
                throw new Exception("Tried to access a property without a string value");
            }
            if (refc.Type != ValueTypes.OBJECT) {
                throw new Exception("Tried to access a property on something other than an object reference.");
            }

            ObjectReference reference = refc.ObjectReference_Value;
            String name = propName.String_Value;
            try {
                val = reference.Home.GetObjectProperty(reference.Index, name);
            } catch (Exception e) {
                vm.RaiseError(e.Message);
                return val;
            }
            return val;
        }

        internal static void Swap(VirtualMachine vm, Instruction i) {
            vm.Stack.Swap();
        }

        internal static void Debug(VirtualMachine vm, Instruction i) {
            String reason = "";
            if (i.Args.Count() > 0) {
                object o = i.Args[0].Get();
                reason = o.ToString();               
            }
            vm.RaiseDebug(reason);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        internal static void Halt(VirtualMachine vm, Instruction i) {
            vm.RaiseHalt("");
            vm.ContinueExecution = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        internal static void Abort(VirtualMachine vm, Instruction i) {
            String msg = "";
            if (i.Args.Count() > 0) {
                msg = i.Args[0].Get().ToString();
            }
            vm.RaiseError(msg);
            vm.ContinueExecution = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        internal static void Clone(VirtualMachine vm, Instruction i) {
            Value prot = vm.Stack.Shift();
            if (prot.Type != ValueTypes.OBJECT) {
                vm.RaiseError("Tried to clone a non-object reference");
                return;
            }
            Prototype p = new Prototype();
            p.Parent = prot.ObjectReference_Value.Home;
            p.ParentIndex = prot.ObjectReference_Value.Index;
            Value v = vm.CurrentEnvironment.AddObject(p);
            vm.Stack.Push(v);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Copy(VirtualMachine vm, Instruction i) {
            Value prot = vm.Stack.Shift();
            if (prot.Type != ValueTypes.OBJECT) {
                vm.RaiseError("Tried to copy a non-object reference");
                return;
            }
            Prototype p = prot.ObjectReference_Value.GetObject();
            Value v = vm.CurrentEnvironment.AddObject(p);
            vm.Stack.Push(v);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        internal static void Pop(VirtualMachine vm, Instruction i) {
            vm.Stack.Pop();
        }

        /// <summary>
        /// Looks for a variable bound to name 'name' in the current environment and returns it.
        /// name (end) | value (end)
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        internal static void GetLocal(VirtualMachine vm, Instruction i) {
            Value name = vm.Stack.Shift();
            if (name.Type != ValueTypes.STRING || name.String_Value == null || name.String_Value == "") {
                vm.RaiseError("Tried to access a bound value with a non-string name");
                return;
            }
            if (vm.CurrentEnvironment.Exists(name.String_Value) == false) {
                vm.RaiseError("Tried to access non-existent value name " + name.String_Value);
                return;
            }
            Value val = vm.CurrentEnvironment.Get(name.String_Value);
            vm.Stack.Push(val);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Delete(VirtualMachine vm, Instruction i) {
            Value name = vm.Stack.Shift();
            if (name.Type != ValueTypes.STRING || name.String_Value == null || name.String_Value == "") {
                vm.RaiseError("Tried to access a bound value with a non-string name");
                return;
            }
            Environment cEnv = vm.CurrentEnvironment;
            while (cEnv != null) {
                if (cEnv.Exists(name.String_Value)) {
                    cEnv.Variables.Remove(name.String_Value);
                }
            }
            vm.RaiseError("Could not find variable " + name.String_Value + " in any environment");
            return;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Get(VirtualMachine vm, Instruction i) {
            Value name = vm.Stack.Shift();
            if (name.Type != ValueTypes.STRING || name.String_Value == null || name.String_Value == "") {
                vm.RaiseError("Tried to access a bound value with a non-string name");
                return;
            }
            Environment cEnv = vm.CurrentEnvironment;
            while (cEnv != null) {
                if (cEnv.Exists(name.String_Value)) {
                    vm.Stack.Push(cEnv.Get(name.String_Value));
                    return;
                }
                cEnv = cEnv.Parent;
            }
            vm.RaiseError("Could not find variable " + name.String_Value + " in any environment");
            return;
        }

        internal static void VarExists(VirtualMachine vm, Instruction i) {
            Value name = vm.Stack.Shift();
            if (name.Type != ValueTypes.STRING || name.String_Value == null || name.String_Value == "") {
                vm.RaiseError("Tried to find a bound value with a non-string name");
                return;
            }
            Environment cEnv = vm.CurrentEnvironment;
            while (cEnv != null) {
                if (cEnv.Exists(name.String_Value)) {
                    vm.Stack.Push(Value.New(ValueTypes.BOOLEAN, true));
                    return;
                }
                cEnv = cEnv.Parent;
            }
            vm.Stack.Push(Value.New(ValueTypes.BOOLEAN, false));
            return;
        }


        internal static void SetNear(VirtualMachine vm, Instruction i) {
            Value vName = vm.Stack.Shift();
            Value vValue = vm.Stack.Shift();
            if (vName.Type != ValueTypes.STRING || vName.String_Value == null || vName.String_Value == "") {
                vm.RaiseError("Tried to use a non-string value for a variable name");
                return;
            }

            if (vm.Exists(vName.String_Value) == false) {
                if (vm.CurrentEnvironment.Parent == null || vm.CurrentEnvironment.Parent.Exists(vName.String_Value) == false) {
                    vm.RaiseError("Tried to access non-existent value name " + vName.String_Value);
                    return;
                }
                if (i.Type == OpCodeTypes.TYPESET) {
                    if (vm.CurrentEnvironment.Parent.Get(vName.String_Value).Type != vValue.Type) {
                        vm.RaiseError("Tried to redefine type for variable " + vName.String_Value + ": value is " + vm.Get(vName.String_Value).Type + ", new value is " + vValue.Type);
                        return;
                    }
                }
                vm.CurrentEnvironment.Parent.Set(vName.String_Value, vValue);
                vm.Stack.Push(vName);
                return;
            } else {
                if (i.Type == OpCodeTypes.TYPESET) {
                    if (vm.Get(vName.String_Value).Type != vValue.Type) {
                        vm.RaiseError("Tried to redefine type for variable " + vName.String_Value + ": value is " + vm.Get(vName.String_Value).Type + ", new value is " + vValue.Type);
                        return;
                    }
                }
                vm.Set(vName.String_Value, vValue);
                vm.Stack.Push(vName);
            }
        }

        internal static void GetGlobal(VirtualMachine vm, Instruction i) {
            Value name = vm.Stack.Shift();
            if (name.Type != ValueTypes.STRING || name.String_Value == null || name.String_Value == "") {
                vm.RaiseError("Tried to access a global bound value with a non-string name");
                return;
            }
            if (vm.GlobalsExists(name.String_Value) == false) {
                vm.RaiseError("Tried to access non-existent global bound value name " + name.String_Value);
                return;
            }
            vm.Stack.Push(vm.GetGlobal(name.String_Value));
            return;
        }

        internal static void SetGlobal(VirtualMachine vm, Instruction i) {
            Value vName = vm.Stack.Shift();
            Value vValue = vm.Stack.Shift();
            if (vName.Type != ValueTypes.STRING || vName.String_Value == null || vName.String_Value == "") {
                vm.RaiseError("Tried to use a non-string value for a variable name");
                return;
            }
            if (vm.GlobalsExists(vName.String_Value) && i.Type == OpCodeTypes.TYPESET_TOP) {
                Value vTop = vm.GetGlobal(vName.String_Value);
                if (vTop.Type != vValue.Type) {
                    vm.RaiseError("Tried to redefine type for value " + vName.String_Value + ": variable is " + vTop.Type + ", new value is " + vValue.Type);
                    return;
                }
            }
            vm.SetGlobal(vName.String_Value, vValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void ArraySet(VirtualMachine vm, Instruction i) {
            Value vIndex = vm.Stack.Shift();
            Value vArray = vm.Stack.Shift();
            Value vValue = vm.Stack.Shift();

            if (vArray.Type != ValueTypes.ARRAY && vArray.Type != ValueTypes.STRING) {
                vm.RaiseError("Tried to index set a non-array and non-string type " + vArray.Type);
                return;
            }

            if (vIndex.IsNumeric() == false || vIndex.Type == ValueTypes.FLOAT || vIndex.Type == ValueTypes.DOUBLE || vIndex.Type == ValueTypes.DECIMAL) {
                vm.RaiseError("Tried to index gsetet an array or string with a non-integer type " + vIndex.Type);
                return;
            }

            Int32 index = Convert.ToInt32(vIndex.Get());

            if (vArray.Type == ValueTypes.ARRAY) {
                if (index >= vArray.Array_Value.Count()) {
                    vm.RaiseError("Tried to set index " + index + " on an array of max index " + (vArray.Array_Value.Count() - 1));
                    return;
                }
                vArray.Array_Value[index] = vValue;
            } else if (vArray.Type == ValueTypes.STRING) {
                if (index > vArray.String_Value.Count()) {
                    vm.RaiseError("Tried to set index " + index + " on a string of max index " + (vArray.Array_Value.Count() - 1));
                    return;
                }
                String tmpStr = vArray.String_Value.Substring(0, index);
                tmpStr = tmpStr + vValue.String_Value[0];
                tmpStr = tmpStr + vArray.String_Value.Substring(index + 1);
                vArray.String_Value = tmpStr;
            }
            vm.Stack.Push(vArray);
            return;
        }

        internal static void StackSize(VirtualMachine vm, Instruction i) {
            Value v = Value.New(ValueTypes.INT_32, (vm.Stack.Position + 1));
            vm.Stack.Push(v);
            return;
        }

        internal static void AppendString(VirtualMachine vm, Instruction i) {
            Value vSecond = vm.Stack.Shift();
            Value vFirst = vm.Stack.Shift();
            Value ret = Value.New(ValueTypes.STRING);
            ret.String_Value = vFirst.Get().ToString() + vSecond.Get().ToString();
            vm.Stack.Push(ret);
        }

        internal static void SpliceString(VirtualMachine vm, Instruction i) {
            Value vIndex = vm.Stack.Shift();
            Value vArray = vm.Stack.Shift();
            Value vValue = vm.Stack.Shift();

            if (vArray.Type != ValueTypes.STRING) {
                vm.RaiseError("Tried to splice a non-string type " + vArray.Type);
                return;
            }

            if (vIndex.IsNumeric() == false || vIndex.Type == ValueTypes.FLOAT || vIndex.Type == ValueTypes.DOUBLE || vIndex.Type == ValueTypes.DECIMAL) {
                vm.RaiseError("Tried to splice a string with a non-integer type " + vIndex.Type);
                return;
            }

            Int32 index = Convert.ToInt32(vIndex.Get());

            if (index > vArray.String_Value.Count()) {
                vm.RaiseError("Tried to splice index " + index + " on a string of max index " + (vArray.Array_Value.Count() - 1));
                return;
            }

            String tmpStr = "";
            if (index == 0) {
                tmpStr = vValue.Get().ToString() + vArray.String_Value;
            } else if (index == vArray.String_Value.Count()) {
                tmpStr = vArray.String_Value + vValue.Get().ToString();
            } else {
                tmpStr = vArray.String_Value.Substring(0, index) + vValue.String_Value + vArray.String_Value.Substring(index);
            }
            vArray.String_Value = tmpStr;
            vm.Stack.Push(vArray);
        }

        /// <summary>
        /// 
        /// (array) (index) (end) | (value) (end)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void ArrayGet(VirtualMachine vm, Instruction i){
            Value vIndex = vm.Stack.Shift();
            Value vArray = vm.Stack.Shift();

            if (vArray.Type != ValueTypes.ARRAY && vArray.Type != ValueTypes.STRING) {
                vm.RaiseError("Tried to index get a non-array and non-string type " + vArray.Type);
                return;
            }

            if(vIndex.IsNumeric() == false || vIndex.Type == ValueTypes.FLOAT || vIndex.Type == ValueTypes.DOUBLE || vIndex.Type == ValueTypes.DECIMAL){
                vm.RaiseError("Tried to index get an array with a non-integer type " + vIndex.Type);
                return;
            }

            Int32 index = Convert.ToInt32(vIndex.Get());
            Value v = new Value();
            if (vArray.Type == ValueTypes.ARRAY) {
                if (index >= vArray.Array_Value.Count()) {
                    vm.RaiseError("Tried to get index " + index + " on an array of max index " + (vArray.Array_Value.Count() - 1));
                }
                v = vArray.Array_Value[index];
            } else if (vArray.Type == ValueTypes.STRING) {
                if (index >= vArray.String_Value.Count()) {
                    vm.RaiseError("Tried to get index " + index + " on a string of max index " + (vArray.Array_Value.Count() - 1));
                }
                v = Value.New(ValueTypes.STRING, vArray.String_Value[index]);
            }
            vm.Stack.Push(v);
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Append(VirtualMachine vm, Instruction i) {
            Value vArray = vm.Stack.Shift();
            Value vValue = vm.Stack.Shift();

            switch(vArray.Type){
                case ValueTypes.ARRAY:
                    if (i.Type == OpCodeTypes.APPEND) {
                        vArray.Array_Value.Add(vValue);
                        vm.Stack.Push(vArray);
                    } else if (i.Type == OpCodeTypes.APPEND_RANGE) {
                        if (vValue.Type == ValueTypes.ARRAY) {
                            vArray.Array_Value.AddRange(vValue.Array_Value);
                            vm.Stack.Push(vArray);
                        } else {
                            vm.RaiseError("Tried to range append using a non-array value. Use APPEND instead.");
                        }
                    } else {
                        vm.RaiseError("Tried append with unknown opcode " + i.Type);
                    }                    
                    return;
                case ValueTypes.STRING:
                    if (i.Type == OpCodeTypes.APPEND) {
                        vm.RaiseError("Tried to append to a string. Use APPEND_RANGE instead.");
                        return;
                    }
                    vArray.String_Value = vArray.String_Value + vValue.Get().ToString();                    
                    vm.Stack.Push(vArray);
                    return;
                default:
                     if (i.Type == OpCodeTypes.APPEND) {
                        vm.RaiseError("Tried to append to a string conversion. Use APPEND_RANGE instead.");
                        return;
                    }
                    Value vNew = new Value();
                    vNew.String_Value = vArray.Get().ToString() + vValue.Get().ToString();
                    vNew.Type = ValueTypes.STRING;
                    vm.Stack.Push(vNew);
                     return;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void GetVar(VirtualMachine vm, Instruction i) {
            if (i.Args.Count() < 1) {
                vm.RaiseError("Tried to get a variable argument without an argument.");
                return;
            }
            if (i.Args[0].Type != ValueTypes.STRING) {
                vm.RaiseError("Tried to get a variable argument with a non-string argument.");
                return;
            }
            Environment cEnv = vm.CurrentEnvironment;
            while (cEnv != null) {
                if (cEnv.Exists(i.Args[0].String_Value)) {
                    vm.Stack.Push(cEnv.Get(i.Args[0].String_Value));
                    return;
                }
                cEnv = cEnv.Parent;
            }
            vm.RaiseError("Could not find variable " + i.Args[0].String_Value + " in any environment");
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void SetVar(VirtualMachine vm, Instruction i) {
            if (i.Args.Count() < 1) {
                vm.RaiseError("Tried to set a variable argument without an argument.");
                return;
            }
            Value vName = i.Args[0];
            Value vValue = Value.New(ValueTypes.NULL);
            if(i.Args.Count() > 1){
                vValue = i.Args[1];
            } else {
                vValue = vm.Stack.Shift();                
            }
            if (vName.Type != ValueTypes.STRING || vName.String_Value == null || vName.String_Value == "") {
                vm.RaiseError("Tried to use a non-string value for a variable argument name");
                return;
            }

            Environment e = vm.CurrentEnvironment;
            while (e != null) {
                if (e.Exists(vName.String_Value)) {
                    break;
                }
                e = e.Parent;
            }
            if (e == null) {
                e = vm.CurrentEnvironment;
            } else {
                if (i.Type == OpCodeTypes.TYPESET) {
                    Value typeValue = e.Get(vName.String_Value);
                    if (typeValue.Type != ValueTypes.ARRAY && typeValue.Type != vValue.Type) {
                        vm.RaiseError("Tried to redefine type for variable " + vName.String_Value + ": value is " + vm.Get(vName.String_Value).Type + ", new value is " + vValue.Type);
                        return;
                    }
                }
            }           
            e.Set(vName.String_Value, vValue);
            vm.Stack.Push(vName);
        }

        /// <summary>
        /// value, name (end) | name (end)
        /// </summary>
        /// <param name="vm"></param>
        internal static void Set(VirtualMachine vm, Instruction i) {
            Value vName = vm.Stack.Shift();
            Value vValue = vm.Stack.Shift();
            if (vName.Type != ValueTypes.STRING || vName.String_Value == null || vName.String_Value == "") {
                vm.RaiseError("Tried to use a non-string value for a variable name");
                return;
            }

            Environment e = vm.CurrentEnvironment;
            while (e != null) {
                if (e.Exists(vName.String_Value)) {
                    break;
                }
                e = e.Parent;
            }
            if (e == null) {
                e = vm.CurrentEnvironment;
            } else {
                if (i.Type == OpCodeTypes.TYPESET) {
                    if (e.Get(vName.String_Value).Type != vValue.Type) {
                        vm.RaiseError("Tried to redefine type for variable " + vName.String_Value + ": value is " + vm.Get(vName.String_Value).Type + ", new value is " + vValue.Type);
                        return;
                    }
                }
            }
            e.Set(vName.String_Value, vValue);
            vm.Stack.Push(vName);
        }

        /// <summary>
        /// Sets the value of the variable pointed to by a Reference.
        /// value, ref (end) | ref (end)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void SetReference(VirtualMachine vm, Instruction i) {
            Value vRef = vm.Stack.Shift();
            Value vValue = vm.Stack.Shift();

            if (vRef.Type != ValueTypes.REFERENCE) {
                vm.RaiseError("Attempted to set the far value of non-reference type " + vRef.Type);
                return;
            }
            Reference refc = vRef.Reference_Value;
            if (refc.HomeEnvironment == null) {
                vm.RaiseError("Attempted to set the far value of an out-of-scope environment");
                return;
            }
            if (refc.HomeEnvironment.Exists(refc.Name) == false) {
                vm.RaiseError("Tried to set via reference undefined value " + refc.Name);
                return;
            }
            if (i.Type == OpCodeTypes.TYPESET_REF) {
                Value vCurr = refc.HomeEnvironment.Get(refc.Name);
                if (vCurr.Type != vValue.Type) {
                    vm.RaiseError("Tried to redefine via reference type for variable " + refc.Name + ": value is " + vCurr.Type + ", new value is " + vValue.Type);
                    return;
                }
            }
            refc.HomeEnvironment.Set(refc.Name, vValue);
            vm.Stack.Push(vRef);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void GetReference(VirtualMachine vm, Instruction i) {
            Value vRef = vm.Stack.Shift();

            if (vRef.Type != ValueTypes.REFERENCE) {
                vm.RaiseError("Attempted to get the far value of non-reference type " + vRef.Type);
                return;
            }
            Reference refc = vRef.Reference_Value;
            if (refc.HomeEnvironment == null) {
                vm.RaiseError("Attempted to get the far value of an out-of-scope environment");
                return;
            }
            if (refc.HomeEnvironment.Exists(refc.Name) == false) {
                vm.RaiseError("Tried to get via reference undefined value " + refc.Name);
                return;
            }
            Value vVal = refc.HomeEnvironment.Get(refc.Name);
            vm.Stack.Push(vVal);
        }


/**** Removed due to needless duplication of effort. SET handles this. Languages can use EXIST and TYEPSET if they don't want to redefine values. ****/
/********************************************
        /// <summary>
        /// type, name (end) | name (end)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void Let(VirtualMachine vm, Instruction i) {
            Value vName = vm.Stack.Shift();
            Value vType = vm.Stack.Shift();
            if (vName.Type != ValueTypes.STRING || vName.String_Value == null || vName.String_Value == "") {
                vm.RaiseError("Tried to use a non-string value for a variable name");
                return;
            }
            if (i.Type == OpCodeTypes.TYPELET) {
                if (vm.Exists(vName.String_Value)) {
                    vm.RaiseError("Tried to define already-defined value " + vName.String_Value);
                    return;
                }
            }
            vm.Let(vName.String_Value, vType.Type);
            vm.Stack.Push(vName);
        }

        /// <summary>
        /// LETSET is a one-pass version of the LET and SET opcodes.
        /// value, name (end) | name (end)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="i"></param>
        internal static void LetSet(VirtualMachine vm, Instruction i) {
            Value vName = vm.Stack.Shift();
            Value vType = vm.Stack.Shift();
            if (vName.Type != ValueTypes.STRING || vName.String_Value == null || vName.String_Value == "") {
                vm.RaiseError("Tried to use a non-string value for a variable name");
                return;
            }
            if (i.Type == OpCodeTypes.TYPELETSET) {
                if (vm.Exists(vName.String_Value)) {
                    vm.RaiseError("Tried to define already-defined value " + vName.String_Value);
                    return;
                }
            }
            vm.Let(vName.String_Value, vType.Type);
            vm.Set(vName.String_Value, vType); 
            vm.Stack.Push(vName);
        }
***********************/

        internal static void Push(VirtualMachine vm, Instruction i) {
            for (int x = 0; x < i.Args.Count(); x++) {
                vm.Stack.Push(i.Args[x]);
                vm.Ticks++;
            }
        }
    }
}

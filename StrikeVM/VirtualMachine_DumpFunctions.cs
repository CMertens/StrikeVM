using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public partial class VirtualMachine {

        public void DumpOperation(System.IO.TextWriter tw) {
            tw.WriteLine("State: " + this.State);
            tw.WriteLine("PC: " + this.ByteCode.ProgramCounter);
            String op = "Operation: ";
            op = op + this.ByteCode.CurrentInstruction.Type + "(";
            for (int x = 0; x < this.ByteCode.CurrentInstruction.Args.Count(); x++) {
                if (x > 0) {
                    op = op + ",";
                }
                op = op + this.ByteCode.CurrentInstruction.Args[x].Type + "=" + this.ByteCode.CurrentInstruction.Args[x].Get().ToString();
            }

            op = op + ")";
            tw.WriteLine(op);
        }

        public void DumpClosures(System.IO.TextWriter tw) {
            tw.WriteLine(this.ClosureStack.Count() + " outer environments on the closure return stack.");
        }

        public void DumpPrimitives(System.IO.TextWriter tw) {
            foreach (String key in Primitives.Keys) {
                StringBuilder sb = new StringBuilder();
                sb.Append(key);
                sb.Append("(");
                for (int x = 0; x < PrimitivesArity[key].Count(); x++) {
                    if (x > 0) {
                        sb.Append(", ");
                    }
                    sb.Append(PrimitivesArity[key][x].Type);
                }
                sb.Append(")");
                Console.WriteLine(sb.ToString());
            }
            tw.WriteLine("--------------------------------------------------------------------------------");
        }

        public void DumpGlobals(System.IO.TextWriter tw) {
            foreach (String key in Globals.Keys) {
                Object val = CurrentEnvironment.Variables[key].Get();
                if (val == null) {
                    val = new String(new char[6] { '<', 'n', 'u', 'l', 'l', '>' });
                }
                Console.WriteLine(key + " :: <" + CurrentEnvironment.Variables[key].Type + "> = " + val.ToString());
            }
            tw.WriteLine("--------------------------------------------------------------------------------");
        }

        public void DumpErrors(System.IO.TextWriter tw) {
            foreach (String s in Errors) {
                tw.WriteLine(s);
            }
            tw.WriteLine("--------------------------------------------------------------------------------");
        }

        public void DumpCurrentEnvironment(System.IO.TextWriter tw) {
            int depth = 0;
            Environment e = CurrentEnvironment;
            while (e.Parent != null) {
                depth++;
                e = e.Parent;
            }
            Console.WriteLine("Environment depth: " + depth);
            foreach (String key in this.CurrentEnvironment.Variables.Keys) {
                Object val = CurrentEnvironment.Variables[key].Get();
                if (val == null) {
                    val = new String(new char[6] { '<', 'n', 'u', 'l', 'l', '>' });
                }
                Console.WriteLine(key + " :: <" + CurrentEnvironment.Variables[key].Type + "> = " + val.ToString());
            }
            tw.WriteLine("--------------------------------------------------------------------------------");
        }

        public void DumpStack(System.IO.TextWriter tw) {
            tw.WriteLine("Stack allocation " + this.Stack.Count() + ", stack position: " + this.Stack.Position);
            tw.WriteLine("(Stack Bottom)");
            for (var x = 0; (x < this.Stack.Count() && x <= this.Stack.Position); x++) {
                String val = "";
                if (this.Stack[x].Get() != null) {
                    val = this.Stack[x].Get().ToString();
                }
                tw.WriteLine((this.Stack.Position - x) + "\t" + this.Stack[x].Type + "\t\t" + val);
            }
            tw.WriteLine("(Stack Top)");
            tw.WriteLine("--------------------------------------------------------------------------------");
        }
    }
}

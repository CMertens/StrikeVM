using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrikeVM;

namespace StrikeAsm
{
    public class AsmCmd
    {
        public String OpCode;
        public List<AsmValue> Args;

        public static AsmCmd Parse(String cmd) {
            throw new NotFiniteNumberException();
        }

        public Instruction Convert() {
            Instruction i = new Instruction();
            i.Type = (OpCodeTypes)Enum.Parse(typeof(OpCodeTypes), OpCode);
            i.Args = new List<Value>();
            foreach (AsmValue av in Args) {
                i.Args.Add(av.Convert());
            }
            return i;
        }
    }
}

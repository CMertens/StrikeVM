using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public class CodeBlock {
        public List<Value> ArgumentsArity;
        public Environment Closure = null;
        public int StartProgramCounter = 0;
        public int EndProgramCounter = 0;
        public CodeBlock() {
            ArgumentsArity = new List<Value>();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("CodeBlock(");
            for (int x = 0; x < ArgumentsArity.Count(); x++) {
                if (x > 0) {
                    sb.Append(",");
                }
                sb.Append(ArgumentsArity[x].Type);
            }
            sb.Append(")");
            sb.Append(" Range: ");
            sb.Append(StartProgramCounter);
            sb.Append("-");
            sb.Append(EndProgramCounter);
            return sb.ToString();
        }
    }
}

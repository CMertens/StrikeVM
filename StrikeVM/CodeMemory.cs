using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    internal class CodeMemory : List<Instruction> {
//        internal Value[] Constants = null;
        internal Dictionary<String, int> Labels;
        internal int ProgramCounter = 0;

        internal CodeMemory() : base(){
            Labels = new Dictionary<string, int>();
        }

        internal CodeMemory(IEnumerable<Instruction> ins)
            : base() {
                Labels = new Dictionary<string, int>();
                foreach (var i in ins) {
                    this.Add(i);
                }
        }

        /// <summary>
        /// Scans the bytecode text and looks for any labels, then binds them to their position
        /// in the text. Used for JUMP_LABEL opcodes.
        /// </summary>
        internal void BindLabels() {
            Labels = new Dictionary<string, int>();
            for (int x = 0; x < this.Count(); x++) {
                if (this[x].Type == OpCodeTypes.LABEL) {
                    if (this[x].Args.Count() > 0) {
                        Labels.Add(this[x].Args[0].Get().ToString(), x);
                    }
                }
            }
        }

        internal bool IsValidInstruction {
            get {
                return ProgramCounter >= 0 && ProgramCounter < this.Count();
            }
        }

        internal Instruction CurrentInstruction {
            get {
                return this[ProgramCounter];
            }
        }
        
    }
}

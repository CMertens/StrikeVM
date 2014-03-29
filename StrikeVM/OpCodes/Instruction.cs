using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public struct Instruction {
        public OpCodeTypes Type;
        public List<Value> Args;


        public Instruction(OpCodeTypes typ) {
            this.Type = typ;
            Args = new List<Value>();
        }

        public Instruction(OpCodeTypes typ, params Value[] values) {
            this.Type = typ;
            Args = new List<Value>();
            foreach (Value v in values) {
                Args.Add(v);
            }
        }

        public static Instruction From(OpCodeTypes typ, Value[] values) {
            Instruction i = new Instruction();
            i.Type = typ;
            i.Args = new List<Value>();
            foreach (Value v in values) {
                i.Args.Add(v);
            }
            return i;
        }
    }
}

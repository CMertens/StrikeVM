using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public class Stack<T> : List<T>{
        public int Position = -1;
        
        public void Push(T v) {
            Position++;
            if (base.Count <= Position) {
                this.Add(v);
            } else {
                this[Position] = v;
            }            
        }

        public void Pop() {
            if (Position >= 0) {
                Position--;
            } else {
                throw new Exception("Tried to run off the edge of the stack");
            }
        }
        public void Pop(int num) {
            Position = Position - num;
        }


        public T Shift() {
            T v = this[Position];
            Position--;
            return v;
        }

        public void Swap() {
            T v = this[Position];
            this[Position] = this[Position - 1];
            this[Position - 1] = v;
        }

        public new int Count() {    
            return Position+1;
        }


    }
}

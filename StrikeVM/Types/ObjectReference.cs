using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    /// <summary>
    /// An ObjectReference is a reference to a Prototype residing in a particular Environment.
    /// </summary>
    public class ObjectReference {
        public Environment Home = null;
        public int Index = -1;

        public Prototype GetObject() {
            if (Home != null) {
                return Home.Prototypes[Index];
            } else {
                return null;
            }
        }

        public override string ToString() {
            return ("ObjectReference{Index:" + Index.ToString() + "}");
        }
    }
}

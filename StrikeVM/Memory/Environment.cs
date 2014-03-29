using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public class Environment {
        public Frame Variables;
        public Environment Parent;
        public Stack<Value> Stack;
        public int EnvironmentID = 0;

        public int Memory = 0;

        // Prototypes are the only reference-stored objects in the VM.
        // An ObjectReference is a pointer to an Environment and the index
        // of the Objects array; the value in Objects points to an index
        // in Prototypes. We use double-indirection to simplify garbage-collection
        // routines.
        public List<int> Objects;
        public List<Prototype> Prototypes;

        public Environment() {
            Variables = new Frame();
            Stack = new Stack<Value>();
            Objects = new List<int>();
            Prototypes = new List<Prototype>();
        }

        /// <summary>
        /// Garbage-collection routine for Prototypes.
        /// </summary>
        public void Collect() {
        }

        /// <summary>
        /// Finds an open slot in Prototypes and adds the requested prototype to it. Returns
        /// an ObjectReference Value pointing to the Prototype.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Value AddObject(Prototype p) {
            Value v = new Value();
            v.Type = ValueTypes.OBJECT;
            v.ObjectReference_Value = new ObjectReference();
            int protLoc = -1;
            for (int x = 0; x < Prototypes.Count(); x++) {
                if (Prototypes[x] == null) {
                    Prototypes[x] = p;
                    protLoc = x;
                    break;
                }
            }
            if (protLoc == -1) {
                Prototypes.Add(p);
                protLoc = Prototypes.Count() - 1;
                Memory = Memory + 2;
            }
            Objects.Add(protLoc);
            v.ObjectReference_Value.Home = this;
            v.ObjectReference_Value.Index = Objects.Count() - 1;
            Memory++;
            return v;
        }

        /// <summary>
        /// Sets 'property' to 'val' on the Prototype denoted locally by 'index'.
        /// Properties are *always* set *locally*, though they may be retrieved
        /// from other environment scopes.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="property"></param>
        /// <param name="val"></param>
        public void SetObjectProperty(int index, string property, Value val) {
            int loc = Objects[index];
            Prototype prot = Prototypes[loc];
            prot.Add(property, val);
            Memory++;
        }

        /// <summary>
        /// Attempts to bind 'property' to the Prototype denoted locally by 'index'.
        /// Will walk the prototype's parent chain upwards until either a matching
        /// property is discovered, or until we hit the final parent and it doesn't
        /// have a bound value.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public Value GetObjectProperty(int index, string property) {
            int loc = Objects[index];
            Prototype prot = Prototypes[loc];
            if (prot.ContainsKey(property) == false) {
                if (prot.Parent == null) {
                    throw new Exceptions.StrikePropertyNotFoundException("Property " + property + " could not be found!");
                }
                // Recursive walk up the prototype chain until we find the property or the final parent
                // says it doesn't have anything to declare.
                return prot.Parent.GetObjectProperty(prot.ParentIndex, property);
            } else {
                return prot[property];
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the variable name exists as a bound name in this Environment.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(String name) {
            if (Variables.ContainsKey(name)) {
                return true;
            }
            return false;
        }

        public void Let(String name, ValueTypes valueType) {
            Variables.Add(name, Value.New(valueType));
            Memory++;
        }

        public void Set(String name, Value val) {
            Variables[name] = val;
        }       

        public Value Get(String name) {
            return Variables[name];
        }
    }
}

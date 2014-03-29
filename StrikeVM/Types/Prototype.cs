using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public class Prototype : Dictionary<String, Value> {
        public Environment Parent = null;
        public int ParentIndex = -1;

        /// <summary>
        /// Starting from the top-level parent.
        /// </summary>
        /// <returns></returns>
        public Prototype Copy() {
            Prototype proto = this;
            List<Prototype> protos = new List<Prototype>();
            protos.Add(proto);
            while (proto.Parent != null) {
                proto = proto.Parent.Prototypes[proto.ParentIndex];
                protos.Add(proto);
            }
            proto = new Prototype();
            for (int x = protos.Count() - 1; x >= 0; x--) {
                foreach (String key in protos[x].Keys) {
                    this.Add(key, protos[x][key]);
                }
            }
            return proto;
        }
    }
}

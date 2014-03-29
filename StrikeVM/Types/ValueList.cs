using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public class ValueList : List<Value> {
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (var x = 0; x < this.Count(); x++) {
                if (x > 0) {
                    sb.Append(",");
                }
                Value v = this[x];
                sb.Append("<");
                sb.Append(v.Type);
                sb.Append("> = ");
                sb.Append(v.Get().ToString());
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}

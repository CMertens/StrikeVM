using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM.Exceptions {
    internal class StrikePropertyNotFoundException : System.Exception {
        internal StrikePropertyNotFoundException() : base(){
        }

        internal StrikePropertyNotFoundException(String msg)
            : base(msg) {
        }
    }
}

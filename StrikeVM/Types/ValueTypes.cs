using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public enum ValueTypes {
        NULL,
        EMPTY,
        BOOLEAN,
        BYTE,
        INT_16,
        INT_32,
        INT_64,
        DECIMAL,
        FLOAT,
        DOUBLE,
        STRING,
        GUID,
        DATETIME,

        /// <summary>
        /// Arrays are Lists of Values.
        /// </summary>
        ARRAY,

        OBJECT,
        CODE_BLOCK,
        REFERENCE,
        UINT_16,
        UINT_32,
        UINT_64,

        ANY_TYPE
    }
}

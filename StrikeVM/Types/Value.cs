using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {

    public struct Value {
        public ValueTypes Type;

        public Int16 Int16_Value;
        public Int32 Int32_Value;
        public Int64 Int64_Value;
        public UInt16 UInt16_Value;
        public UInt32 UInt32_Value;
        public UInt64 UInt64_Value;
        public byte Byte_Value;
        public string String_Value;
        public decimal Decimal_Value;
        public bool Boolean_Value;
        public ValueList Array_Value;
        public ObjectReference ObjectReference_Value;
        public CodeBlock CodeBlock_Value;
        public Reference Reference_Value;
        public float Float_Value;
        public double Double_Value;
        public Guid Guid_Value;
        public DateTimeOffset DateTime_Value;

        public static Value New(ValueTypes vt){
            Value v = new Value();
            v.Type = vt;
            switch (vt) {
                case ValueTypes.STRING:
                    v.String_Value = "";
                    break;
                case ValueTypes.ARRAY:
                    v.Array_Value = new ValueList();
                    break;
                case ValueTypes.CODE_BLOCK:
                    v.CodeBlock_Value = new CodeBlock();
                    break;
                    break;
                default:
                    break;
            }
            return v;
        }

        public static Value New(ValueTypes vt, Object obj) {
            Value v = new Value();
            v.Type = vt;
            switch (v.Type) {
                case ValueTypes.ARRAY:
                    v.Array_Value = new ValueList();
                    break;
                case ValueTypes.BOOLEAN:
                    v.Boolean_Value = Convert.ToBoolean(obj);
                    break;
                case ValueTypes.BYTE:
                    v.Byte_Value = Convert.ToByte(obj);
                    break;
                case ValueTypes.CODE_BLOCK:
                    v.CodeBlock_Value = (CodeBlock)obj;
                    break;
                case ValueTypes.DATETIME:
                    throw new NotImplementedException();
                    break;
                case ValueTypes.DECIMAL:
                    v.Decimal_Value = Convert.ToDecimal(obj);
                    break;
                case ValueTypes.DOUBLE:
                    v.Double_Value = Convert.ToDouble(obj);
                    break;
                case ValueTypes.EMPTY:
                    break;
                case ValueTypes.FLOAT:
                    v.Float_Value = Convert.ToSingle(obj);
                    break;
                case ValueTypes.GUID:
                    throw new NotImplementedException();
                    break;
                case ValueTypes.INT_16:
                    v.Int16_Value = Convert.ToInt16(obj);
                    break;
                case ValueTypes.INT_32:
                    v.Int32_Value = Convert.ToInt32(obj);
                    break;
                case ValueTypes.INT_64:
                    v.Int64_Value = Convert.ToInt64(obj);
                    break;
                case ValueTypes.NULL:
                    break;
                case ValueTypes.OBJECT:
                    throw new NotImplementedException();
                    break;
                case ValueTypes.REFERENCE:
                    v.Reference_Value = new Reference();
                    break;
                case ValueTypes.STRING:
                    v.String_Value = Convert.ToString(obj);
                    break;
                case ValueTypes.UINT_16:
                    v.UInt16_Value = Convert.ToUInt16(obj);
                    break;
                case ValueTypes.UINT_32:
                    v.UInt32_Value = Convert.ToUInt32(obj);
                    break;
                case ValueTypes.UINT_64:
                    v.UInt64_Value = Convert.ToUInt64(obj);
                    break;
                case ValueTypes.ANY_TYPE:
                    break;
            }
            return v;
        }

        public List<byte> SerializeToBinary() {
            throw new NotImplementedException();
            List<byte> s = new List<byte>();

            byte[] typeBytes = BitConverter.GetBytes((Int32)this.Type);
            s.AddRange(typeBytes);

            if (BitConverter.IsLittleEndian) {
                s.Reverse();
            }
            return s;
        }

        public byte[] GetAsBytes() {
            throw new NotImplementedException();
        }

        public Object Get() {
            switch (Type) {
                case ValueTypes.ARRAY:
                    return Array_Value;
                case ValueTypes.BOOLEAN:
                    return Boolean_Value;
                case ValueTypes.BYTE:
                    return Byte_Value;
                case ValueTypes.CODE_BLOCK:
                    return CodeBlock_Value;
                case ValueTypes.DECIMAL:
                    return Decimal_Value;
                case ValueTypes.EMPTY:
                    return null;
                case ValueTypes.INT_16:
                    return Int16_Value;
                case ValueTypes.INT_32:
                    return Int32_Value;
                case ValueTypes.INT_64:
                    return Int64_Value;
                case ValueTypes.NULL:
                    return null;
                case ValueTypes.REFERENCE:
                    return Reference_Value;
                case ValueTypes.STRING:
                    return String_Value;
                case ValueTypes.OBJECT:
                    return ObjectReference_Value;
                case ValueTypes.UINT_16:
                    return UInt16_Value;
                case ValueTypes.UINT_32:
                    return UInt32_Value;
                case ValueTypes.UINT_64:
                    return UInt64_Value;
                case ValueTypes.FLOAT:
                    return Float_Value;
                case ValueTypes.DOUBLE:
                    return Double_Value;
                case ValueTypes.GUID:
                    return Guid_Value;
                case ValueTypes.DATETIME:
                    return DateTime_Value;
                default:
                    return null;
            }
            throw new Exception("Attempt to get unknown type " + this.Type);
        }

        public bool IsNumeric() {
            if (this.Type == ValueTypes.DECIMAL || this.Type == ValueTypes.DOUBLE || this.Type == ValueTypes.FLOAT || this.Type == ValueTypes.INT_16 || this.Type == ValueTypes.INT_32 || this.Type == ValueTypes.INT_64 || this.Type == ValueTypes.UINT_16 || this.Type == ValueTypes.UINT_32 || this.Type == ValueTypes.UINT_64) {
                return true;
            }
            return false;
        }

        public static ValueType HighestValue(ValueTypes a, ValueTypes b) {
            throw new NotImplementedException();
            switch (a) {
                case ValueTypes.ARRAY:
                    return ValueTypes.NULL;
                case ValueTypes.BOOLEAN:
                    return ValueTypes.NULL;
                case ValueTypes.BYTE:
                    return ValueTypes.NULL;
                case ValueTypes.CODE_BLOCK:
                    return ValueTypes.NULL;
                case ValueTypes.DATETIME:
                    return ValueTypes.NULL;
                case ValueTypes.DECIMAL:
                    return ValueTypes.NULL;
                case ValueTypes.DOUBLE:
                    return ValueTypes.NULL;
                case ValueTypes.EMPTY:
                    return ValueTypes.NULL;
                case ValueTypes.FLOAT:
                    return ValueTypes.NULL;
                case ValueTypes.GUID:
                    return ValueTypes.NULL;
                case ValueTypes.INT_16:
                    return ValueTypes.NULL;
                case ValueTypes.INT_32:
                    return ValueTypes.NULL;
                case ValueTypes.INT_64:
                    return ValueTypes.NULL;
                case ValueTypes.NULL:
                    return ValueTypes.NULL;
                case ValueTypes.REFERENCE:
                    return ValueTypes.NULL;
                case ValueTypes.STRING:
                    return ValueTypes.NULL;
                case ValueTypes.OBJECT:
                    return ValueTypes.NULL;
                case ValueTypes.UINT_16:
                    return ValueTypes.NULL;
                case ValueTypes.UINT_32:
                    return ValueTypes.NULL;
                case ValueTypes.UINT_64:
                    return ValueTypes.NULL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vt"></param>
        public void ConvertValue(ValueTypes vt) {
            switch (this.Type) {
                case ValueTypes.ARRAY:
                    return;
                case ValueTypes.BOOLEAN:
                    return;
                case ValueTypes.BYTE:
                    return;
                case ValueTypes.CODE_BLOCK:
                    return;
                case ValueTypes.DATETIME:
                    return;
                case ValueTypes.DECIMAL:
                    return;
                case ValueTypes.DOUBLE:
                    return;
                case ValueTypes.EMPTY:
                    return;
                case ValueTypes.FLOAT:
                    return;
                case ValueTypes.GUID:
                    return;
                case ValueTypes.INT_16:
                    return;
                case ValueTypes.INT_32:
                    return;
                case ValueTypes.INT_64:
                    return;
                case ValueTypes.NULL:
                    return;
                case ValueTypes.REFERENCE:
                    return;
                case ValueTypes.STRING:
                    return;
                case ValueTypes.OBJECT:
                    return;
                case ValueTypes.UINT_16:
                    return;
                case ValueTypes.UINT_32:
                    return;
                case ValueTypes.UINT_64:
                    return;
            }
        }

        ///
        ///
        public void Commensurate(ValueTypes vt) {
            switch (this.Type) {
                case ValueTypes.ARRAY:

                    return;
                case ValueTypes.BOOLEAN:
                    return;
                case ValueTypes.BYTE:
                    return;
                case ValueTypes.CODE_BLOCK:
                    return;
                case ValueTypes.DATETIME:
                    return;
                case ValueTypes.DECIMAL:
                    return;
                case ValueTypes.DOUBLE:
                    return;
                case ValueTypes.EMPTY:
                    return;
                case ValueTypes.FLOAT:
                    return;
                case ValueTypes.GUID:
                    return;
                case ValueTypes.INT_16:
                    return;
                case ValueTypes.INT_32:
                    return;
                case ValueTypes.INT_64:
                    return;
                case ValueTypes.NULL:
                    return;
                case ValueTypes.REFERENCE:
                    return;
                case ValueTypes.STRING:
                    return;
                case ValueTypes.OBJECT:
                    return;
                case ValueTypes.UINT_16:
                    return;
                case ValueTypes.UINT_32:
                    return;
                case ValueTypes.UINT_64:
                    return;
            }
        }

        /// <summary>
        /// Promotes a Value if appropriate. 
        /// float -> double
        /// int -> int
        /// uint -> uint
        /// Attempts to promote other types will throw an exception.
        /// </summary>
        /// <param name="val"></param>
        public void Promote(Value val) {
            switch (this.Type) {
                case ValueTypes.DECIMAL:
                    switch (val.Type) {
                        case ValueTypes.DECIMAL:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.DOUBLE:
                    switch (val.Type) {
                        case ValueTypes.DOUBLE:
                        case ValueTypes.FLOAT:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.FLOAT:
                    switch (val.Type) {
                        case ValueTypes.DOUBLE:
                            this.Type = ValueTypes.DOUBLE;
                            this.Double_Value = Convert.ToDouble(this.Float_Value);
                            this.Float_Value = 0;
                            return;
                        case ValueTypes.FLOAT:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.INT_16:
                    switch (val.Type) {
                        case ValueTypes.INT_64:
                            this.Type = ValueTypes.INT_64;
                            this.Int64_Value = Convert.ToInt64(this.Int16_Value);
                            this.Int16_Value = 0;
                            return;
                        case ValueTypes.INT_32:
                            this.Type = ValueTypes.INT_32;
                            this.Int32_Value = Convert.ToInt32(this.Int16_Value);
                            this.Int16_Value = 0;
                            return;
                        case ValueTypes.INT_16:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.INT_32:
                    switch (val.Type) {
                        case ValueTypes.INT_64:
                            this.Type = ValueTypes.INT_64;
                            this.Int64_Value = Convert.ToInt64(this.Int16_Value);
                            this.Int16_Value = 0;
                            return;
                        case ValueTypes.INT_32:
                        case ValueTypes.INT_16:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.INT_64:
                    switch (val.Type) {
                        case ValueTypes.INT_64:
                        case ValueTypes.INT_32:
                        case ValueTypes.INT_16:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.UINT_16:
                    switch (val.Type) {
                        case ValueTypes.UINT_64:
                            this.Type = ValueTypes.UINT_64;
                            this.UInt64_Value = Convert.ToUInt64(this.UInt16_Value);
                            this.UInt16_Value = 0;
                            return;
                        case ValueTypes.UINT_32:
                            this.Type = ValueTypes.UINT_32;
                            this.UInt32_Value = Convert.ToUInt32(this.UInt16_Value);
                            this.UInt16_Value = 0;
                            return;
                        case ValueTypes.UINT_16:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.UINT_32:
                    switch (val.Type) {
                        case ValueTypes.UINT_64:
                            this.Type = ValueTypes.UINT_64;
                            this.UInt64_Value = Convert.ToUInt64(this.UInt16_Value);
                            this.UInt16_Value = 0;
                            return;
                        case ValueTypes.UINT_32:
                        case ValueTypes.UINT_16:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
                case ValueTypes.UINT_64:
                    switch (val.Type) {
                        case ValueTypes.UINT_64:
                        case ValueTypes.UINT_32:
                        case ValueTypes.UINT_16:
                            return;
                        default:
                            throw new Exception("Attempt to promote using invalid types " + this.Type + " and " + val.Type + ".");
                    }
                    break;
            }
        }

        public static Value CreateFrom(Object o) {
            Value v = new Value();
            if (o.GetType() == typeof(Int16)){
                v.Type = ValueTypes.INT_16;
                v.Int16_Value = (Int16)o;
            } else if (o.GetType() == typeof(Int32)) {
                v.Type = ValueTypes.INT_32;
                v.Int32_Value = (Int32)o;
            } else if (o.GetType() == typeof(Int64)) {
                v.Type = ValueTypes.INT_64;
                v.Int64_Value = (Int64)o;
            } else if (o.GetType() == typeof(UInt16)) {
                v.Type = ValueTypes.UINT_16;
                v.UInt16_Value = (UInt16)o;
            } else if (o.GetType() == typeof(UInt32)) {
                v.Type = ValueTypes.UINT_32;
                v.UInt32_Value = (UInt32)o;
            } else if (o.GetType() == typeof(UInt64)) {
                v.Type = ValueTypes.UINT_64;
                v.UInt64_Value = (UInt64)o;
            } else if (o.GetType() == typeof(byte)) {
                v.Type = ValueTypes.BYTE;
                v.Byte_Value = (byte)o;
            } else if (o.GetType() == typeof(decimal)) {
                v.Type = ValueTypes.DECIMAL;
                v.Decimal_Value = (decimal)o;
            } else if (o.GetType() == typeof(string)) {
                v.Type = ValueTypes.STRING;
                v.String_Value = (string)o;
            } else if (o.GetType() == typeof(ValueList)) {
                v.Type = ValueTypes.ARRAY;
                v.Array_Value = (ValueList)o;
            } else if (o.GetType() == typeof(ObjectReference)) {
                v.Type = ValueTypes.OBJECT;
                v.ObjectReference_Value = (ObjectReference)o;
            } else if (o.GetType() == typeof(CodeBlock)) {
                v.Type = ValueTypes.CODE_BLOCK;
                v.CodeBlock_Value = (CodeBlock)o;            
            } else if (o.GetType() == typeof(Reference)) {
                v.Type = ValueTypes.REFERENCE;
                v.Reference_Value = (Reference)o;
            } else {
                throw new Exception();
            }
            return v;
        }
    }
}

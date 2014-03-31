using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrikeVM;
using System.Text.RegularExpressions;

namespace StrikeAsm {
    public class Assembler {

        public Assembler() {
        }

        public List<Instruction> Compile(String code, bool useDebug) {
            List<Instruction> isl = new List<Instruction>();

            using (System.IO.StringReader sr = new System.IO.StringReader(code)) {
                String line = String.Empty;
                int x = 1;
                do {
                    line = sr.ReadLine();
                    if (line != null) {
                        try {
                            isl.Add(Parse(line, x));
                        } catch (InvalidCastException) {
                            // This just means there's no instruction to return.
                        } catch (Exception e) {
                            Console.WriteLine("(" + x + ") " + e.Message);
                            System.Environment.Exit(-111);
                        }
                    }
                    x++;
                } while (line != null);
            }
            return isl;
        }

        internal Instruction Parse(String line, int lineNumber) {
            Instruction i = new Instruction();
            i.Args = new List<Value>();

            
            List<Value> args = new List<Value>();
            
            // Strip comments (Anything that starts with * that isn't in a string literal)
            String inst = "";
            int quotes = 0;
            try {
                for (int cs = 0; cs < line.Length; cs++) {
                    if (line[cs] == '"' && line[cs - 1] != '\\') {
                        quotes++;
                    }
                    if ((line[cs] == '*' || line[cs] == ';') && (quotes == 0 || quotes % 2 == 0)) {
                        break;
                    }
                    inst = inst + line[cs];
                }
            } catch (Exception e) {
                throw new Exception("Line number: " + lineNumber + ", " + e.Message);
            }
            quotes = 0;
            String OpCode = "";
            int c = 0;            

            // Skip starting whitespace
            while (c < inst.Length && (inst[c] == ' ' || inst[c] == '\t')) {
                c++;
            }
            if (inst.Length < 1 || inst.StartsWith("*") || c >= inst.Length) {
                throw new InvalidCastException();
            }

            // Find opcode
            while (c < inst.Length) {
                if (inst[c] == ' ' || inst[c] == '\t') {
                    break;
                }
                OpCode = OpCode + inst[c];
                c++;
            }
            // Parse out the opcode or blow up
            try {
                i.Type = (OpCodeTypes)Enum.Parse(typeof(OpCodeTypes), OpCode);                
            } catch (Exception e) {
                throw new Exception("Illegal opcode " + OpCode + " at line " + lineNumber);
            }            

            // Skip any intervening whitespace
            while (c < inst.Length && (inst[c] == ' ' || inst[c] == '\t')) {
                c++;
            }
            if (c >= inst.Length) {
                return i;
            }

            // Get arguments (type(:value)? (,type(:value)?)+)
            while (c < inst.Length) {
                Value v = new Value();
                String vType = "";
                String vValue = "";
                while (c < inst.Length && (inst[c] == ' ' || inst[c] == '\t')) {
                    c++;
                }                
                // Get type
                while (c < inst.Length && inst[c] != ':' && inst[c] != ',') {
                    vType = vType + inst[c];
                    c++;
                }
                if (vType.ToLowerInvariant() == "int16") {
                    v.Type = ValueTypes.INT_16;
                } else if (vType.ToLowerInvariant() == "int32") {
                    v.Type = ValueTypes.INT_32;
                } else if (vType.ToLowerInvariant() == "int64") {
                    v.Type = ValueTypes.INT_64;
                } else if (vType.ToLowerInvariant() == "uint16") {
                    v.Type = ValueTypes.UINT_16;
                } else if (vType.ToLowerInvariant() == "uint32") {
                    v.Type = ValueTypes.UINT_32;
                } else if (vType.ToLowerInvariant() == "uint64") {
                    v.Type = ValueTypes.UINT_64;
                } else if (vType.ToLowerInvariant() == "null") {
                    v.Type = ValueTypes.NULL;
                } else if (vType.ToLowerInvariant() == "empty") {
                    v.Type = ValueTypes.EMPTY;
                } else if (vType.ToLowerInvariant() == "boolean") {
                    v.Type = ValueTypes.BOOLEAN;
                } else if (vType.ToLowerInvariant() == "byte") {
                    v.Type = ValueTypes.BYTE;
                } else if (vType.ToLowerInvariant() == "decimal") {
                    v.Type = ValueTypes.DECIMAL;
                } else if (vType.ToLowerInvariant() == "float") {
                    v.Type = ValueTypes.FLOAT;
                } else if (vType.ToLowerInvariant() == "double") {
                    v.Type = ValueTypes.DOUBLE;
                } else if (vType.ToLowerInvariant() == "string") {
                    v.Type = ValueTypes.STRING;
                } else if (vType.ToLowerInvariant() == "guid") {
                    v.Type = ValueTypes.GUID;
                } else if (vType.ToLowerInvariant() == "datetime") {
                    v.Type = ValueTypes.DATETIME;
                } else if (vType.ToLowerInvariant() == "array") {
                    v.Type = ValueTypes.ARRAY;
                } else if (vType.ToLowerInvariant() == "object") {
                    v.Type = ValueTypes.OBJECT;
                } else if (vType.ToLowerInvariant() == "code_block") {
                    v.Type = ValueTypes.CODE_BLOCK;
                } else if (vType.ToLowerInvariant() == "reference") {
                    v.Type = ValueTypes.REFERENCE;
                } else if (vType.ToLowerInvariant() == "any") {
                    v.Type = ValueTypes.ANY_TYPE;
                } else {
                    throw new Exception("Unknown type " + vType);
                }


                
                // If there's no literal value, add a new Value to the list and go to the next one
                if (c >= inst.Length || inst[c] == ',') {
                    i.Args.Add(v);
                    c++;
                    continue;
                }
                
                if (c < inst.Length && (inst[c] != ',' && inst[c] != ':')) {
                    throw new Exception("Malformed value " + inst[c] + " at line " + lineNumber);
                }
                if (c < inst.Length && inst[c] == ':') {
                    c++;
                }
                // Ignore whitespace
                while (c < inst.Length && (inst[c] == ' ' || inst[c] == '\t')) {
                    c++;
                }
                while (c < inst.Length && (inst[c] != ',' || (inst[c] == ',' && (quotes != 0 && quotes % 2 != 0)))) {
                    vValue = vValue + inst[c];
                    c++;
                }

                try {
                    // Figure out what the value is
                    if (Regex.IsMatch(vValue, @"True|False")) {
                        // Boolean
                        if (v.Type == ValueTypes.BOOLEAN) {
                            v.Boolean_Value = Convert.ToBoolean(vValue);
                        } else if (v.Type == ValueTypes.STRING) {
                            vValue = vValue.Substring(1, vValue.Length - 2);
                            vValue.Replace("\"\"", "\"");
                            v.String_Value = vValue;
                        } else {
                            throw new Exception("Tried to set literal " + vValue + " to value type " + v.Type);
                        }
                    } else if (Regex.IsMatch(vValue, @"[-]?[0-9]+\.[0-9]+")) {
                        // Decimal
                        if (v.Type == ValueTypes.DECIMAL) {
                            v.Decimal_Value = Convert.ToDecimal(vValue);
                        } else if (v.Type == ValueTypes.DOUBLE) {
                            v.Double_Value = Convert.ToDouble(vValue);
                        } else if (v.Type == ValueTypes.FLOAT) {
                            v.Float_Value = Convert.ToSingle(vValue);
                        } else if (v.Type == ValueTypes.STRING) {
                            vValue = vValue.Substring(1, vValue.Length - 2);
                            vValue.Replace("\"\"", "\"");
                            v.String_Value = vValue;
                        } else {
                            throw new Exception("Tried to set literal " + vValue + " to value type " + v.Type);
                        }
                    } else if (Regex.IsMatch(vValue, @"[-]?[0-9][0-9]*") || Regex.IsMatch(vValue, @"0x[0-9A-Fa-f]+") || Regex.IsMatch(vValue, @"0[0-7]+")) {
                        if (v.Type == ValueTypes.INT_16) {
                            v.Int16_Value = Convert.ToInt16(vValue);
                        } else if (v.Type == ValueTypes.INT_32) {
                            v.Int32_Value = Convert.ToInt32(vValue);
                        } else if (v.Type == ValueTypes.INT_64) {
                            v.Int64_Value = Convert.ToInt64(vValue);
                        } else if (v.Type == ValueTypes.UINT_16) {
                            v.UInt16_Value = Convert.ToUInt16(vValue);
                        } else if (v.Type == ValueTypes.UINT_32) {
                            v.UInt32_Value = Convert.ToUInt32(vValue);
                        } else if (v.Type == ValueTypes.UINT_64) {
                            v.UInt64_Value = Convert.ToUInt64(vValue);
                        } else if (v.Type == ValueTypes.BYTE) {
                            v.Byte_Value = Convert.ToByte(vValue);
                        } else if (v.Type == ValueTypes.STRING) {
                            vValue = vValue.Substring(1, vValue.Length - 2);
                            vValue.Replace("\"\"", "\"");
                            v.String_Value = vValue;
                        } else {
                            throw new Exception("Tried to set literal " + vValue + " to value type " + v.Type);
                        }
                    } else {
                        v = Value.New(v.Type, v.String_Value);
                        vValue = vValue.Substring(1, vValue.Length - 2);
                        vValue.Replace("\"\"", "\"");
                        v.String_Value = vValue;
                    }
                    i.Args.Add(v);
                    if (c < inst.Length && inst[c] == ',') {
                        c++;
                    }
                    while (c < inst.Length && (inst[c] == ' ' || inst[c] == '\t')) {
                        c++;
                    }
                } catch (Exception e) {
                    throw new Exception("Compile error on line number: " + lineNumber + ", " + e.Message);
                }
            }

            return i;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrikeVM;

namespace StrikeAsm {
    public class Assembler {
        Tokenizer tokenizer;

        public Assembler() {
            tokenizer = Tokenizer.Create()
                .Boundary(@"[ \f\n\r\t\v]+")
                .Token(0, "COLON", @"\:")
                .Token(0, "SEMICOLON", @"\;")
                .Token(0, "COMMA", @",")

                .Token(1, "BOOLEAN", @"Boolean")
                .Token(1, "STRING", @"String")
                .Token(1, "INT_16", @"Int16")
                .Token(1, "INT_32", @"Int32")
                .Token(1, "INT_64", @"Int64")
                .Token(1, "UINT_16", @"UInt16")
                .Token(1, "UINT_32", @"UInt32")
                .Token(1, "UINT_64", @"UInt64")
                .Token(1, "DECIMAL", @"Decimal")
                .Token(1, "FLOAT", @"Float")
                .Token(1, "DOUBLE", @"Double")
                .Token(1, "ARRAY", @"Array")


                .Token(2, "DECIMAL_NUMBER", @"[-]?[0-9]+\.[0-9]+")
                .Token(2, "HEX_NUMBER", @"0x[0-9A-Fa-f]+")
                .Token(2, "OCTAL_NUMBER", @"0[0-7]+")
                .Token(2, "ANNOTATION", @"\@[A-Za-z_][A-Za-z0-9_]*[=[A-Za-z_0-9]+]?")
                .Token(2, "TYPE", @"\<[A-Za-z_][A-Za-z0-9_\-\.]*\>")

                .Token(3, "NUMBER", @"[-]?[0-9][0-9]*")
                .Token(3, "QUOTED_STRING", @"(?<unicode>n)?""(?<value>(?:""""|[^""])*)""")
                .Token(3, "PARTIAL_STRING", @"(?<unicode>n)?""(?<value>(?:""""|[^""])*)")

                .Token(3, "ID", @"[A-Za-z_][A-Za-z0-9_]*")
                .Ignore(3, "ML_COMMENT", @"(?s)/\*.*?\*/")
                //.Ignore(3, "SL_COMMENT", @"(?s)//.*$")
                .Build();
        }

        public List<Instruction> Compile(String code, bool useDebug) {
            var tokenslist = tokenizer.Tokenize(code);
            List<Instruction> isl = new List<Instruction>();


            int location = 0;
            Token[] tokens = tokenslist.ToArray();
            if (useDebug) {
                foreach (var t in tokens) {
                    Console.WriteLine(t.Name + ":" + t.Literal);
                }
            }

            for (int x = 0; x < tokens.Count(); x++) {
                location++;
                Instruction i = new Instruction();
                OpCodeTypes op = (OpCodeTypes)Enum.Parse(typeof(OpCodeTypes),tokens[x].Literal.ToUpperInvariant());
                x++;
                if (tokens[x].Name.ToString() != "SEMICOLON" ) {                    
                    List<Value> args = new List<Value>();
                    do {
                        if (((String)tokens[x].Name.ToString()) == "COMMA") {
                            x++;
                        }
                        ValueTypes val = (ValueTypes)Enum.Parse(typeof(ValueTypes), Convert.ToString(tokens[x].Name));
                        x++;
                        if (tokens[x].Name.ToString() == "SEMICOLON") {
                            args.Add(Value.New(val));
                            continue;
                        }
                        if (tokens[x].Name.ToString() != "COLON") {
                            throw new Exception("Line " + location + ": Error parsing argument (missing colon).");
                        }
                        x++;
                        String s = tokens[x].Literal;
                        x++;
                        if (val == ValueTypes.STRING) {
                            s = s.Substring(1, s.Length - 2);
                            s.Replace("\"\"", "\"");
                        }
                        args.Add(Value.New(val, s));                        
                    } while ( x < tokens.Count() && ((String)tokens[x].Name.ToString()) == "COMMA");
                    i = Instruction.From(op, args.ToArray());
                } else {
                    i = new Instruction(op);
                }
                isl.Add(i);
            }
            return isl;
        }
    }
}

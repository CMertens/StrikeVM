using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace StrikeAsm {
    public struct Token {
        public IConvertible Name;
        public String Literal;
        public String State;
        public int LineNumber;
    }

    public class Tokenizer {
        Dictionary<IConvertible, Regex> tokens = new Dictionary<IConvertible, Regex>();
        List<Regex> bounds = new List<Regex>();
        Dictionary<IConvertible, int> tokenPriorities = new Dictionary<IConvertible, int>();
        Dictionary<IConvertible, bool> ignore = new Dictionary<IConvertible, bool>();

        public static Tokenizer Create() {
            return (new Tokenizer());
        }

        public Tokenizer Token(int priority, IConvertible tokenName, String tokenRegex) {
            try {
                Regex.Replace("", tokenRegex, "");
                tokenPriorities[tokenName] = priority;
                tokens[tokenName] = new Regex("^" + tokenRegex + "$");
            } catch (Exception e) {
                throw (e);
            }
            return (this);
        }

        public Tokenizer Ignore(int priority, IConvertible tokenName, String tokenRegex) {
            try {
                Regex.Replace("", tokenRegex, "");
                tokenPriorities[tokenName] = priority;
                tokens[tokenName] = new Regex("^" + tokenRegex + "$");
                ignore[tokenName] = true;
            } catch (Exception e) {
                throw (e);
            }
            return (this);
        }

        public Tokenizer Boundary(String boundaryRegex) {
            try {
                Regex.Replace("", boundaryRegex, "");
                bounds.Add(new Regex("^" + boundaryRegex + "$"));
            } catch (Exception e) {
                throw (e);
            }
            return (this);
        }

        public Tokenizer Build() {
            return (this);
        }

        private enum TokenizerStates {
            NONE,
            START_OF_STREAM,
            IN_TOKEN,
            IN_BOUNDARY,
            WRITE_TOKEN,
            END_OF_STREAM
        }

        private enum MatchTypes {
            NONE,
            BOUNDARY,
            TOKEN
        }

        public IEnumerable<Token> Tokenize(String stream) {
            List<Token> tlist = new List<Token>();
            if (stream.Length < 1) {
                return (new Token[0]);
            }
            int loc = 0;

            while (loc < stream.Length) {
                MatchTypes match = MatchTypes.NONE;
                int lookahead = stream.Length + 1;

                while (match == MatchTypes.NONE) {
                    lookahead--;
                    lookahead = Math.Min(lookahead, stream.Length - loc);
                    if (lookahead < 1) {
                        throw new Exception("Line " + (1 + stream.Substring(0, loc).Count(c => c == System.Environment.NewLine[0])) + ": (" + loc + "," + lookahead + ") Unresolvable token: " + stream.Substring(loc));
                    }
                    //Console.WriteLine("Attempting match to: " + stream.Substring(loc, lookahead));
                    match = Match(stream.Substring(loc, lookahead));
                }
                //Console.WriteLine("Match: " + match + " at " + loc + ", len " + lookahead);
                switch (match) {
                    case MatchTypes.BOUNDARY:
                        loc = loc + lookahead;
                        break;
                    case MatchTypes.TOKEN:
                        Token t = (new Token() {
                            Literal = stream.Substring(loc, lookahead),
                            Name = MatchToken(stream.Substring(loc, lookahead)),
                            LineNumber = (1 + stream.Substring(0, lookahead).Count(c => c == System.Environment.NewLine[0]))
                        });
                        if (ignore.ContainsKey(t.Name) == false) {
                            tlist.Add(t);
                        }
                        loc = loc + lookahead;
                        break;
                    case MatchTypes.NONE:
                    default:
                        throw new Exception("Line " + (1 + stream.Substring(0, loc).Count(c => c == System.Environment.NewLine[0])) + ": (" + loc + "," + lookahead + ") Unresolvable token: " + stream.Substring(loc, lookahead));
                }
            }
            return (tlist);
        }

        public IEnumerable<Token> TokenizeOneAhead(String stream) {
            List<Token> tlist = new List<Token>();
            if (stream.Length < 1) {
                return (new Token[0]);
            }
            String buffer = "";
            int x = 0;
            for (x = 0; x < stream.Length; x++) {

                if (Match(buffer + stream[x]) == MatchTypes.NONE) {
                    switch (Match(buffer)) {
                        case MatchTypes.BOUNDARY:
                            buffer = stream[x].ToString();
                            break;
                        case MatchTypes.TOKEN:
                            tlist.Add(new Token() {
                                Literal = buffer,
                                Name = MatchToken(buffer),
                                LineNumber = (1 + stream.Substring(0, x).Count(c => c == System.Environment.NewLine[0]))
                            });
                            buffer = stream[x].ToString();
                            break;
                        case MatchTypes.NONE:
                            throw new Exception("Line " + (1 + stream.Substring(0, x).Count(c => c == System.Environment.NewLine[0])) + ": Found unmatched token: " + buffer);
                        default:
                            throw new Exception("Line " + (1 + stream.Substring(0, x).Count(c => c == System.Environment.NewLine[0])) + ": Should not reach default case at ::Tokenize(String)");
                    }
                } else {
                    buffer = buffer + stream[x];
                }
            }
            switch (Match(buffer)) {
                case MatchTypes.BOUNDARY:
                    break;
                case MatchTypes.TOKEN:
                    tlist.Add(new Token() {
                        Literal = buffer,
                        Name = MatchToken(buffer),
                        LineNumber = (1 + stream.Substring(0, x).Count(c => c == System.Environment.NewLine[0]))
                    });
                    break;
                case MatchTypes.NONE:
                    throw new Exception("Line " + (1 + stream.Substring(0, x).Count(c => c == System.Environment.NewLine[0])) + ": Found unmatched token: " + buffer);
                default:
                    throw new Exception("Line " + (1 + stream.Substring(0, x).Count(c => c == System.Environment.NewLine[0])) + ": Should not reach default case at ::Tokenize(String)");
            }
            return (tlist);
        }

        private String MatchToken(String buffer) {
            String lastMatch = null;
            foreach (String s in tokenPriorities.Keys) {
                if (lastMatch == null || tokenPriorities[s] < tokenPriorities[lastMatch]) {
                    if (tokens[s].IsMatch(buffer)) {
                        lastMatch = s;
                    }
                }
            }
            return (lastMatch);
        }

        private MatchTypes Match(String buffer) {
            foreach (Regex r in bounds) {
                if (r.IsMatch(buffer)) {
                    return (MatchTypes.BOUNDARY);
                }
            }
            foreach (Regex r in tokens.Values) {
                if (r.IsMatch(buffer)) {
                    return (MatchTypes.TOKEN);
                }
            }
            return (MatchTypes.NONE);
        }

        public String TestTokenString(String token) {
            foreach (String tn in tokenPriorities.Keys) {
                if (tokens[tn].IsMatch(token)) {
                    return (tn);
                }
            }
            return (null);
        }

        public bool TestBoundaryString(String bound) {
            foreach (Regex r in bounds) {
                if (r.IsMatch(bound)) {
                    return (true);
                }
            }
            return (false);
        }

    }
}

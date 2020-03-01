using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.JSON
{
    public static class JSONLex
    {
        static List<char> symbols = new List<char>() { '{', '}', '[', ']', ':', ',' };
        static List<char> whitespaces = new List<char>() { ' ', '\t', '\n', '\r' };

        private static bool IsNumber(string src)
        {
            bool neg = false, dot = false;
            if (src.EndsWith(".")) return false;

            foreach (char c in src)
            {
                if (c == '-')
                {
                    if (neg) return false;
                    neg = true;
                }

                if (c == '.')
                {
                    if (dot) return false;
                    dot = true;
                }

                if (!(c >= '0' && c <= '9')) return false;
            }

            if (neg && src[0] != '-') return false;
            return true;
        }

        private static void AddToList(List<JSONToken> tokens, string s, bool isString = false)
        {
            if (isString)
            {
                tokens.Add(new JSONToken(JSONTokenType.STRING, s));
                return;
            }

            if (s == "") return;

            if (s.Length == 1 && symbols.Contains(s[0]))
                tokens.Add(new JSONToken(JSONTokenType.SYMBOL, s));
            else if (IsNumber(s))
                tokens.Add(new JSONToken(JSONTokenType.NUMBER, s));
            else if (s == "true" || s == "false")
                tokens.Add(new JSONToken(JSONTokenType.BOOL, s));
            else
                throw new NotSupportedException($"Malformed JSON, unknown symbol : {s}.");
        }


        public static List<JSONToken> LexJSON(string src)
        {
            var ret = new List<JSONToken>();
            int len = src.Length;

            string current = "";

            for (int i = 0; i < len; ++i)
            {
                // Whitespaces
                if (whitespaces.Contains(src[i]))
                {
                    if (current != "")
                        AddToList(ret, current);
                    current = "";
                    continue;
                }

                // Symbols, as defined before
                if (symbols.Contains(src[i]))
                {
                    if (current != "")
                        AddToList(ret, current);
                    
                    AddToList(ret, src[i].ToString());

                    current = "";
                    continue;
                }

                // Strings
                if (src[i] == '"')
                {
                    AddToList(ret, current);
                    current = "";

                    ++i;

                    try
                    {
                        while (src[i] != '"')
                        {
                            current += src[i++];
                        }

                        AddToList(ret, current, true);
                        current = "";

                        continue;
                    } 
                    catch (ArgumentOutOfRangeException)
                    {
                        throw new Exception("Expected matching \"");
                    }

                }

                // The booleans
                if (src[i] == 't')
                {
                    AddToList(ret, current);
                    current = "";

                    if (i + 3 >= len)
                        throw new NotSupportedException("Malformed JSON, Expected \"true\".");
                    i += 3;

                    AddToList(ret, "true");
                    continue;
                }
                
                if (src[i] == 'f')
                {
                    AddToList(ret, current);
                    current = "";

                    if (i + 4 >= len)
                        throw new NotSupportedException("Malformed JSON, Expected \"false\".");
                    i += 4;

                    AddToList(ret, "false");
                    continue;
                }

                current += src[i];
            }

            AddToList(ret, current);

            return ret;
        }
    }
}

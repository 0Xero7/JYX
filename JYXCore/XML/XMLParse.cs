using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.XML
{
    public static class XMLParse
    {
        public static Dictionary<string, dynamic> ParseXML(Span<XMLToken> tokens)
        {
            var ret = new Dictionary<string, dynamic>();
            ret[tokens[0].data] = ParseObject(tokens.Slice(1, tokens.Length - 2));

            return ret;
        }

        // We know its an object, so data format is
        // e -> <tag>d</tag>
        // d -> <tag>d</tag> | term
        private static Dictionary<string, dynamic> ParseObject(Span<XMLToken> tokens)
        {
            var ret = new Dictionary<string, dynamic>();

            for (int i = 0; i < tokens.Length; ++i)
            {
                string key = tokens[i].data;
                ++i;

                // <tag>term</tag>
                if (tokens[i].IsData())
                {
                    ret[key] = new Common.ASTString(tokens[i].data);

                    // TODO : check if tag is closed
                    ++i;
                    continue;
                }

                // token is a opening tag

                // new object since key and optag arent same
                if (!tokens[i].IsOpening(key))
                {
                    int begin = i;
                    while (i < tokens.Length && !tokens[i].IsClosing(key)) ++i;

                    ret[key] = ParseObject(tokens.Slice(begin, i - begin));

                    continue;
                }

                // collection since key and optag are same
                if (tokens[i].IsOpening(key))
                {
                    int begin = i, top = 1;
                    while (i < tokens.Length && top > 0)
                    {
                        if (tokens[i].IsClosing(key)) --top;
                        if (tokens[i].IsOpening(key)) ++top;
                        ++i;
                    }

                    ret[key] = ParseCollection(tokens.Slice(begin, i - begin - 1));
                    --i;

                    continue;
                }

                throw new NotSupportedException("Invalid XML format.");
            }

            return ret;
        } 
        
        private static List<dynamic> ParseCollection(Span<XMLToken> tokens)
        {
            var ret = new List<dynamic>();

            for (int i = 0; i < tokens.Length; ++i)
            {
                string key = tokens[i].data;
                ++i;

                // <tag>term</tag>
                if (tokens[i].IsData())
                {
                    ret.Add(new Common.ASTString(tokens[i].data));

                    // TODO : check if tag is closed
                    ++i;
                    continue;
                }

                // token is a opening tag

                // new object since key and optag arent same
                if (!tokens[i].IsOpening(key))
                {
                    int begin = i;
                    while (i < tokens.Length && !tokens[i].IsClosing(key)) ++i;

                    ret.Add(ParseObject(tokens.Slice(begin, i - begin)));

                    continue;
                }

                // collection since key and optag are same
                if (tokens[i].IsOpening(key))
                {
                    int begin = i, top = 1;
                    while (i < tokens.Length && top > 0)
                    {
                        if (tokens[i].IsClosing(key)) --top;
                        if (tokens[i].IsOpening(key)) ++top;
                        ++i;
                    }

                    ret.Add(ParseCollection(tokens.Slice(begin, i - begin - 1)));
                    --i;

                    continue;
                }

                throw new NotSupportedException("Invalid XML format.");
            }

            return ret;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.XML
{
    public static class XMLLex
    {
        static List<char> symbols = new List<char>() { '<', '>', '/' };
        static List<char> whitespaces = new List<char>() { ' ', '\t', '\n', '\r' };

        public static List<XMLToken> LexXML(string src)
        {
            var ret = new List<XMLToken>();
            int len = src.Length;

            string current = "";

            for (int i = 0; i < len - 1; ++i)
            {
                if (src[i] == '<')
                {
                    if (current != "" && src[i+1] == '/')
                        ret.Add(new XMLToken(current, XMLTokenType.DATA));
                    current = "";

                    while (i < len && src[++i] != '>') current += src[i];
                    if (current[0] != '/')
                        ret.Add(new XMLToken(current, XMLTokenType.OPENING));
                    else
                        ret.Add(new XMLToken(current.Substring(1, current.Length - 1), XMLTokenType.CLOSING));

                    if (current[0] == '/')
                    {
                        while (i < len - 1 && whitespaces.Contains(src[++i])) ;
                        --i;
                    }
                    current = "";

                    continue;
                }

                current += src[i];
            }

            /*if (current != "")
                ret.Add(current);*/

            return ret;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.XML
{
    public static class SerializeXML
    {
        public static string Serialize(Dictionary<string, dynamic> ast)
        {
            string res = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>\n" +
                         "<root>\n";

            res += SerializeObject(ast, 1, "   ");

            res += "</root>";

            return res;
        }

        private static string SerializeObject(Dictionary<string, dynamic> ast, int indent, string indentText = "\t")
        {
            string wp = "";
            for (int i = 0; i < indent; ++i) wp += indentText;

            string res = "";
            foreach (var v in ast)
            {
                switch (v.Value)
                {
                    case Common.ASTString s:
                        res += $"{wp}<{v.Key}>{s.value}</{v.Key}>\n";
                        break;
                    case Common.ASTNumber n:
                        res += $"{wp}<{v.Key}>{n.number}</{v.Key}>\n";
                        break;
                    case Common.ASTBoolean b:
                        res += $"{wp}<{v.Key}>{(b.boolean ? "true" : "false")}</{v.Key}>\n";
                        break;

                    case Dictionary<string, dynamic> d:
                        res += $"{wp}<{v.Key}>\n";
                        res += SerializeObject(d, indent + 1, indentText);
                        res += $"{wp}</{v.Key}>\n";
                        break;

                    case List<dynamic> l:
                        res += $"{wp}<{v.Key}>\n";
                        res += SerializeCollection(l, v.Key, indent + 1, indentText);
                        res += $"{wp}</{v.Key}>\n";
                        break;
                }
            }

            return res;
        }

        private static string SerializeCollection(List<dynamic> ast, string parent, int indent, string indentText = "\t")
        {
            string wp = "";
            for (int i = 0; i < indent; ++i) wp += indentText;

            string res = "";
            foreach (var v in ast)
            {
                switch (v)
                {
                    case Common.ASTString s:
                        res += $"{wp}<{parent}>{s.value}</{parent}>\n";
                        break;
                    case Common.ASTNumber n:
                        res += $"{wp}<{parent}>{n.number}</{parent}>\n";
                        break;
                    case Common.ASTBoolean b:
                        res += $"{wp}<{parent}>{(b.boolean ? "true" : "false")}</{parent}>\n";
                        break;

                    case Dictionary<string, dynamic> d:
                        res += $"{wp}<{parent}>\n";
                        res += SerializeObject(d, indent + 1, indentText);
                        res += $"{wp}</{parent}>\n";
                        break;

                    case List<dynamic> l:
                        res += $"{wp}<{parent}>\n";
                        res += SerializeCollection(l, v, indent + 1, indentText);
                        res += $"{wp}</{parent}>\n";
                        break;
                }
            }

            return res;
        }
    }
}

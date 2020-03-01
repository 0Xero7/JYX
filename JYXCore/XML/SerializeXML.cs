using System;
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
                        res += SerializeCollection(l, indent + 1, indentText);
                        res += $"{wp}</{v.Key}>\n";
                        break;
                }
            }

            return res;
        }
        
        private static string SerializeCollection(List<dynamic> ast, int indent, string indentText = "\t")
        {
            string wp = "";
            for (int i = 0; i < indent; ++i) wp += indentText;

            string res = "";
            foreach (var v in ast)
            {
                switch (v)
                {
                    case Common.ASTString s:
                        res += $"{wp}<element>{s.value}</element>\n";
                        break;
                    case Common.ASTNumber n:
                        res += $"{wp}<element>{n.number}</element>\n";
                        break;
                    case Common.ASTBoolean b:
                        res += $"{wp}<element>{(b.boolean ? "true" : "false")}</element>\n";
                        break;

                    case Dictionary<string, dynamic> d:
                        res += $"{wp}<element>\n";
                        res += SerializeObject(d, indent + 1, indentText);
                        res += $"{wp}</element>\n";
                        break;

                    case List<dynamic> l:
                        res += $"{wp}<element>\n";
                        res += SerializeCollection(l, indent + 1, indentText);
                        res += $"{wp}</element>\n";
                        break;
                }
            }

            return res;
        }
    }
}

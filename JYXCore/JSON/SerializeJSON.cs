using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.JSON
{
    public static class SerializeJSON
    {
        public static string Serialize(Dictionary<string, dynamic> ast)
        {
            return $"{{\n{SerializeObject(ast, 1, "   ")}}}";
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
                        res += $"{wp}\"{v.Key}\" : \"{s.value}\",\n";
                        break;
                    case Common.ASTNumber n:
                        res += $"{wp}\"{v.Key}\" : {n.number},\n";
                        break;
                    case Common.ASTBoolean b:
                        res += $"{wp}\"{v.Key}\" : {(b.boolean ? "true" : "false")},\n";
                        break;

                    case Dictionary<string, dynamic> d:
                        res += $"{wp}\"{v.Key}\" : {{\n";
                        res += SerializeObject(d, indent + 1, indentText);
                        res += $"{wp}}},\n";
                        break;

                    case List<dynamic> l:
                        res += $"{wp}\"{v.Key}\" : [\n";
                        res += SerializeCollection(l, indent + 1, indentText);
                        res += $"{wp}],\n";
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
                        res += $"{wp}\"{s.value}\",\n";
                        break;
                    case Common.ASTNumber n:
                        res += $"{wp}{n.number},\n";
                        break;
                    case Common.ASTBoolean b:
                        res += $"{wp}{(b.boolean ? "true" : "false")},\n";
                        break;

                    case Dictionary<string, dynamic> d:
                        res += $"{wp}{{\n";
                        res += SerializeObject(d, indent + 1, indentText);
                        res += $"{wp}}},\n";
                        break;

                    case List<dynamic> l:
                        res += $"{wp}[\n";
                        res += SerializeCollection(l, indent + 1, indentText);
                        res += $"{wp}],\n";
                        break;
                }
            }

            return res;
        }
    }
}

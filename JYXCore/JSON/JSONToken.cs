using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.JSON
{
    public class JSONToken
    {
        public JSONTokenType type;
        public string content;

        public JSONToken(JSONTokenType type, string content)
        {
            this.type = type;
            this.content = content;
        }
    }

    public static class JSONTokenExtension
    {
        public static bool IsColon(this JSONToken arg)
        {
            return arg.content == ":" && arg.type == JSONTokenType.SYMBOL;
        }

        public static bool IsComma(this JSONToken arg)
        {
            return arg.content == "," && arg.type == JSONTokenType.SYMBOL;
        }

        public static bool IsObjectOpening(this JSONToken arg)
        {
            return arg.content == "{" && arg.type == JSONTokenType.SYMBOL;
        }

        public static bool IsObjectClosing(this JSONToken arg)
        {
            return arg.content == "}" && arg.type == JSONTokenType.SYMBOL;
        }

        public static bool IsCollectionOpening(this JSONToken arg)
        {
            return arg.content == "[" && arg.type == JSONTokenType.SYMBOL;
        }

        public static bool IsCollectionClosing(this JSONToken arg)
        {
            return arg.content == "]" && arg.type == JSONTokenType.SYMBOL;
        }
    }
}

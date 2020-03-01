using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.JSON
{
    public static class JSONParse
    {
        // Parses a JSON object ( starting and ending with '{' and '}' )
        public static Dictionary<string, dynamic> ParseJSON(Span<JSONToken> tokens)
        {
            int len = tokens.Length;
            Dictionary<string, dynamic> ret = new Dictionary<string, dynamic>();

            for (int i = 1; i < len - 1; ++i)
            {
                string key = tokens[i].content;
                if (!tokens[++i].IsColon()) throw new NotImplementedException("Invalid JSON expression.");

                switch (tokens[++i].type)
                {
                    case JSONTokenType.STRING:
                        ret[key] = new Common.ASTString(tokens[i].content);

                        if (tokens[i + 1].IsComma())
                            ++i;
                        continue;

                    case JSONTokenType.NUMBER:
                        ret[key] = new Common.ASTNumber(tokens[i].content);

                        if (tokens[i + 1].IsComma())
                            ++i;
                        continue;

                    case JSONTokenType.BOOL:
                        ret[key] = new Common.ASTBoolean(tokens[i].content == "true");

                        if (tokens[i + 1].IsComma())
                            ++i;
                        continue;

                    case JSONTokenType.SYMBOL:
                        if (tokens[i].content == "{")   // Object
                        {
                            int begin = i, stack = 1;
                            ++i;

                            while (stack != 0)
                            {
                                if (tokens[i].IsObjectOpening()) ++stack;
                                if (tokens[i].IsObjectClosing()) --stack;

                                if (stack == 0)
                                {
                                    var sliced = tokens.Slice(begin, i - begin + 1);
                                    ret[key] = ParseJSON(sliced);

                                    ++i;
                                    continue;
                                }
                                ++i;
                            }
                        }
                        else if (tokens[i].content == "[")  // Collection
                        {
                            int begin = i, stack = 1;
                            ++i;

                            while (stack != 0)
                            {
                                if (tokens[i].IsCollectionOpening()) ++stack;
                                if (tokens[i].IsCollectionClosing()) --stack;

                                if (stack == 0)
                                {
                                    var sliced = tokens.Slice(begin, i - begin + 1);
                                    ret[key] = ParseCollection(sliced);

                                    ++i;
                                    continue;
                                }
                                ++i;
                            }
                        }

                        continue;
                }
            }

            return ret;
        }

        public static List<dynamic> ParseCollection(Span<JSONToken> tokens)
        {
            List<dynamic> ret = new List<dynamic>();

            for (int i = 1; i < tokens.Length - 1; ++i)
            {
                switch (tokens[i].type)
                {
                    case JSONTokenType.STRING:
                        ret.Add(new Common.ASTString(tokens[i].content));

                        if (tokens[i + 1].IsComma())
                            ++i;
                        continue;

                    case JSONTokenType.NUMBER:
                        ret.Add(new Common.ASTNumber(tokens[i].content));

                        if (tokens[i + 1].IsComma())
                            ++i;
                        continue;

                    case JSONTokenType.BOOL:
                        ret.Add(new Common.ASTBoolean(tokens[i].content == "true"));

                        if (tokens[i + 1].IsComma())
                            ++i;
                        continue;

                    case JSONTokenType.SYMBOL:
                        if (tokens[i].content == "{")   // Object
                        {
                            int begin = i, stack = 1;
                            ++i;

                            while (stack != 0)
                            {
                                if (tokens[i].IsObjectOpening()) ++stack;
                                if (tokens[i].IsObjectClosing()) --stack;

                                if (stack == 0)
                                {
                                    var sliced = tokens.Slice(begin, i - begin + 1);
                                    ret.Add(ParseJSON(sliced));

                                    ++i;
                                    continue;
                                }
                                ++i;
                            }
                        }
                        else if (tokens[i].content == "[")  // Collection
                        {
                            int begin = i, stack = 1;
                            ++i;

                            while (stack != 0)
                            {
                                if (tokens[i].IsCollectionOpening()) ++stack;
                                if (tokens[i].IsCollectionClosing()) --stack;

                                if (stack == 0)
                                {
                                    var sliced = tokens.Slice(begin, i - begin + 1);
                                    ret.Add(ParseCollection(sliced));

                                    ++i;
                                    continue;
                                }
                                ++i;
                            }
                        }

                        continue;
                }

                if (tokens[i + 1].IsComma()) ++i;
            }

            return ret;
        }
    }
}

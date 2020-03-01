using System;
using System.Collections.Generic;
using System.Collections;

namespace JYXCore
{
    public class JYXCore
    {
        private Dictionary<string, dynamic> obj;
        
        public JYXCore(string source)
        {
            obj = new Dictionary<string, dynamic>();

            var res = JSON.JSONLex.LexJSON(source);
            var parsed = JSON.JSONParse.ParseJSON(res.ToArray());
        }
    }
}

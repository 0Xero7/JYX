using System;
using System.Collections.Generic;
using System.Collections;

namespace JYXCore
{
    public class JYXCore
    {
        private Dictionary<string, dynamic> obj;
        
        public JYXCore(string source, FileType fileType = FileType.JSON)
        {
            obj = new Dictionary<string, dynamic>();

            switch (fileType)
            {
                case FileType.JSON:
                    var res = JSON.JSONLex.LexJSON(source);
                    var parsed = JSON.JSONParse.ParseJSON(res.ToArray());
                    Console.WriteLine(XML.SerializeXML.Serialize(parsed));
                    break;
                case FileType.XML:
                    var xmlLex = XML.XMLLex.LexXML(source);

                    foreach (var s in xmlLex) Console.WriteLine($"{s.type}\t\t: {s.data}");

                    var x = XML.XMLParse.ParseXML(xmlLex.ToArray());

                    Console.WriteLine(JSON.SerializeJSON.Serialize(x));
                    //var parsed = JSON.JSONParse.ParseJSON(res.ToArray());
                    //Console.WriteLine(XML.SerializeXML.Serialize(parsed));
                    break;
            }

            //Console.WriteLine(XML.SerializeXML.Serialize(parsed));
            //Console.WriteLine(JSON.SerializeJSON.Serialize(parsed));
        }
    }
}

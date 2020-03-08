using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.XML
{
    public class XMLCollectorModel
    {
        public string name { get; private set; }
        public dynamic content { get; private set; }

        public XMLCollectorModel(string name, dynamic content)
        {
            this.name = name;
            this.content = content;
        }
    }
}

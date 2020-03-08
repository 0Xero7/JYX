using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.XML
{
    public class XMLToken
    {
        public string data;
        public XMLTokenType type;

        public XMLToken(string data, XMLTokenType type) => (this.data, this.type) = (data, type);

        public bool IsOpening(string name) => data == name && type == XMLTokenType.OPENING;
        public bool IsClosing(string name) => data == name && type == XMLTokenType.CLOSING;
        public bool IsData() => type == XMLTokenType.DATA;
    }
}

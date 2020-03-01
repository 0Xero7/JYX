using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.Common
{
    public class ASTString
    {
        public string value { get; private set; }
        public ASTString(string value)
        {
            this.value = value;
        }
    }
}

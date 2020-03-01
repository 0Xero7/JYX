using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.Common
{
    public class ASTNumber
    {
        public string number { get; private set; }
        public ASTNumber(string number)
        {
            this.number = number;
        }
    }
}

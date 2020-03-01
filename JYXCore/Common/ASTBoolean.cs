using System;
using System.Collections.Generic;
using System.Text;

namespace JYXCore.Common
{
    public class ASTBoolean
    {
        public bool boolean { get; private set; }
        public ASTBoolean(bool state)
        {
            this.boolean = state;
        }
    }
}

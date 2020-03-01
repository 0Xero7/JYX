using System;
using System.Collections.Generic;
using System.IO;
using JYXCore;

namespace XYJ
{
    class Program
    {
        static void Main(string[] args)
        {
            string f = File.ReadAllText(@"C:\Users\smpsm\source\repos\XYJ\XYJ\TextFile1.txt");
            JYXCore.JYXCore s = new JYXCore.JYXCore(f);
        }
    }
}

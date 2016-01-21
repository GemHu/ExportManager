using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CodeAutoGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo info = new DirectoryInfo(@"..\..\Data");
            CodeGenerateManager manager = new CodeGenerateManager(info.FullName);
            manager.Start();
        }
    }
}

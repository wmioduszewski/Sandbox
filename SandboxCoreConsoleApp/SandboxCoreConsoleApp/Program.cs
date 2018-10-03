using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SandboxCoreConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(
            Uri.IsWellFormedUriString("ftp://84.35.65.216", UriKind.Absolute));

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SandboxCoreConsoleApp
{
    class Program
    {
        private static string filepath = @"C:\Users\WojciechMioduszewski\Desktop\input.txt";

        static void Main(string[] args)
        {
            decimal two =0;
            decimal three=0;

            List<string> lines = new List<string>();

            using (var stream = new StreamReader(filepath))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    lines.Add(line);
                }
            }

            Count(lines);

            Console.WriteLine("Complete.");
            Console.ReadKey();
        }

        static void Count(List<string> lines)
        {
            foreach (var left in lines)
            {
                foreach (var right in lines)
                {
                    var res = Compare(left, right);
                    if(res == 1)
                        Console.WriteLine(left + "\r\n" + right + "\r\n\r\n");
                }
            }
        }

        static int Compare(string left, string right)
        {
            int diff = 0;
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                    diff++;
            }

            return diff;
        }


    }


}

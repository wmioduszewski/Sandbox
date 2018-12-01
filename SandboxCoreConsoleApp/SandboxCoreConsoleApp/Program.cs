using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SandboxCoreConsoleApp
{
    class Program
    {
        private static string filepath = @"C:\Users\WojciechMioduszewski\Desktop\input.txt";

        static void Main(string[] args)
        {
            decimal sum = 0m;
            var dict = new HashSet<decimal> { sum };
            int rounds = 1;
            bool res = true;
            while (res)
            {
                using (var reader = new StreamReader(filepath))
                {
                    rounds++;
                    while (!reader.EndOfStream)
                    {
                        sum += Convert.ToDecimal(reader.ReadLine());
                        //Console.WriteLine("skladnik " + sum);
                        res = dict.Add(sum);
                        if(!res) break;
                    }
                }
            }

            Console.WriteLine(sum);
            Console.WriteLine(rounds);

            Console.ReadKey();
        }


    }


}

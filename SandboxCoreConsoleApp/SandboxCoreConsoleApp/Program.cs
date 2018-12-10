using System;
using System.IO;
using System.Text;

namespace SandboxCoreConsoleApp
{
    class Program
    {
        private static string filepath = @"C:\Users\WojciechMioduszewski\Desktop\input.txt";


        static void Main(string[] args)
        {
            int len = 'a'-'A';
            StringBuilder sb = new StringBuilder();
            using (var stream = new StreamReader(filepath))
            {
                sb.Append(stream.ReadLine());
            }

            int previousLen = sb.Length;
            int difference = 1;
            int index = 1;
            while (difference>0)
            {
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    string input1 = (char) i + ((char)(i + len)).ToString();
                    string input2 = (char) (i + len) + ((char) i).ToString();
                    sb.Replace(input1, "");
                    sb.Replace(input2, "");
                }

                difference = previousLen - sb.Length;
                previousLen = sb.Length;

                Console.WriteLine($"{index++} : {difference}");
            }

            Console.WriteLine(sb.Length);
            Console.WriteLine(sb.ToString());
            Console.WriteLine("Complete.");
            Console.ReadKey();
        }

    }


}

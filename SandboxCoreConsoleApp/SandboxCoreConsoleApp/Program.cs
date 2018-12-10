using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace SandboxCoreConsoleApp
{
    class Program
    {
        private static string filepath = @"C:\Users\WojciechMioduszewski\Desktop\input.txt";
        private static int len = 'a' - 'A';

        static void Main(string[] args)
        {
            string originalInput;
            using (var stream = new StreamReader(filepath))
            {
                originalInput = stream.ReadLine();
            }

            int min = int.MaxValue;
            for (int i = 'A'; i <= 'Z'; i++)
            {
                StringBuilder sb = new StringBuilder(originalInput);
                sb.Replace(((char) i).ToString(), "");
                sb.Replace(((char) (i+len)).ToString(), "");
                var res = ReactPolymer(sb.ToString());
                if (res < min)
                {
                    min = res;
                }
            }

            Console.WriteLine(min);
            Console.WriteLine("Complete.");
            Console.ReadKey();
        }

        static int ReactPolymer(string input)
        {
            StringBuilder sb = new StringBuilder(input);
            int previousLen = sb.Length;
            int difference = 1;

            while (difference > 0)
            {
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    string input1 = (char)i + ((char)(i + len)).ToString();
                    string input2 = (char)(i + len) + ((char)i).ToString();
                    sb.Replace(input1, "");
                    sb.Replace(input2, "");
                }

                difference = previousLen - sb.Length;
                previousLen = sb.Length;
            }

            return sb.Length;
        }

    }


}

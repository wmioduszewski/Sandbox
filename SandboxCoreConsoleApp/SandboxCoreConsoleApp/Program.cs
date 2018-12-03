using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SandboxCoreConsoleApp
{
    class Program
    {
        private static string filepath = @"C:\Users\WojciechMioduszewski\Desktop\input.txt";

        static List<Rectangle> rectanles = new List<Rectangle>();

        static void Main(string[] args)
        {
            int size = 1100;
            int [,] board= new int [size,size];
            using (var stream = new StreamReader(filepath))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    var rect = new Rectangle(line);
                    rectanles.Add(rect);

                    ApplyRectOnBoard(board, rect);
                }
            }

            foreach (var rect in rectanles)
            {
                for (int i = rect.X; i < rect.X + rect.Width; i++)
                {
                    if(rect.NotTheOne)
                        break;
                    for (int j = rect.Y; j < rect.Y + rect.Height; j++)
                    {
                        if (board[i, j] != 1)
                        {
                            rect.NotTheOne = true;
                            break;
                        }
                            
                    }
                }
            }
            

            Console.WriteLine(rectanles.First(x=>!x.NotTheOne).Id);

            Console.WriteLine("Complete.");
            Console.ReadKey();
        }

        private static void ApplyRectOnBoard(int[,] board, Rectangle rect)
        {
            for (int i = rect.X; i < rect.X + rect.Width; i++)
            {
                for (int j = rect.Y; j < rect.Y + rect.Height; j++)
                {
                    board[i, j]++;
                }
            }
        }

        class Rectangle
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int Id { get; set; }
            public bool NotTheOne { get; set; }

            public Rectangle(string line)
            {
                var matches = Regex.Match(line, "#(?<id>\\d+) @ (?<x>\\d+),(?<y>\\d+): (?<w>\\d+)x(?<h>\\d+)");
                X = Convert.ToInt32(matches.Groups["x"].Captures[0].Value);
                Y = Convert.ToInt32(matches.Groups["y"].Captures[0].Value);
                Width = Convert.ToInt32(matches.Groups["w"].Captures[0].Value);
                Height = Convert.ToInt32(matches.Groups["h"].Captures[0].Value);
                Id = Convert.ToInt32(matches.Groups["id"].Captures[0].Value);
            }

        }


    }


}

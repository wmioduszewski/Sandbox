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

        static List<Record> records = new List<Record>();

        static void Main(string[] args)
        {
            int size = 1100;
            int[,] board = new int[size, size];
            using (var stream = new StreamReader(filepath))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    var rect = new Record(line);
                    records.Add(rect);
                }
            }

            records = records.OrderBy(x => x.DateTime).ToList();
            AnalyzeRecords();


            Console.WriteLine("Complete.");
            Console.ReadKey();
        }

        private static void AnalyzeRecords()
        {
            var guards = new Dictionary<int, int>();
            Dictionary<int, int[]> guardMinutes = new Dictionary<int, int[]>();
            int guardId = -1;
            int minute = 0;
            foreach (var record in records)
            {

                var matches = Regex.Match(record.Message, "Guard #(?<id>\\d+) begins shift");

                if (matches.Success)
                {
                    guardId = Convert.ToInt32(matches.Groups["id"].Value);
                    minute = 0;
                }

                if (record.Message.Contains("asleep"))
                {
                    minute = record.DateTime.Hour == 23 ? 0 : record.DateTime.Minute;
                }

                if (record.Message.Contains("wakes"))
                {
                    if (guards.ContainsKey(guardId))
                    {
                        guards[guardId] += record.DateTime.Minute - minute;

                        for (int i = minute; i < record.DateTime.Minute; i++)
                        {
                            guardMinutes[guardId][i] += 1;
                        }
                    }
                    else
                    {
                        guards.Add(guardId, record.DateTime.Minute - minute);
                        guardMinutes.Add(guardId, new int[61]);

                        for (int i = minute; i < record.DateTime.Minute; i++)
                        {
                            guardMinutes[guardId][i] += 1;
                        }

                    }
                    minute = 0;
                }
            }

            var guard = guards.OrderByDescending(x => x.Value).First().Key;
            var max = guardMinutes[guard].Max();
            int guardMinute = Array.IndexOf(guardMinutes[guard], max);

            Console.WriteLine(guard * guardMinute);
        }

        class Record
        {
            public DateTime DateTime { get; set; }
            //public int GuardId { get; set; }
            public string Message { get; set; }

            public Record(string line)
            {
                //var matches = Regex.Match(line, "#(?<id>\\d+) @ (?<x>\\d+),(?<y>\\d+): (?<w>\\d+)x(?<h>\\d+)");
                var matches = Regex.Match(line, "\\[(?<datetime>\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2})\\] (?<rest>.*)");
                DateTime = DateTime.Parse(matches.Groups["datetime"].Value);
                Message = matches.Groups["rest"].Captures[0].Value;
            }

        }


    }


}

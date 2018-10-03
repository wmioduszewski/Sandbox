using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SandboxCoreConsoleApp
{
    public static class RegexXml
    {
        private static readonly string SqlCommandNodeName = "SqlCommand";
        private static readonly string Pattern = $@"<\s*{SqlCommandNodeName}\s*>\s*(?<SqlQuery>.*?)\s*<\s*/\s*{SqlCommandNodeName}\s*>";

        public static void Perform()
        {
            string input = File.ReadAllText("XMLFile1.xml");
            Match match = Regex.Match(input, Pattern);
            if (match.Success)
            {
                var sqlQuery = match.Groups["SqlQuery"].Value;

                var sqlCommandNode = match.Groups[0].Value;
                var newSqlCommandNode = Regex.Replace(sqlCommandNode, Regex.Escape(sqlQuery),
                    $"<![CDATA[{sqlQuery}]]>", RegexOptions.IgnorePatternWhitespace);

                var newContent = Regex.Replace(input, Regex.Escape(sqlCommandNode), newSqlCommandNode);

                Console.WriteLine(sqlQuery);
                Console.WriteLine(newContent);
            }
            else
            {
                Console.WriteLine("no");
            }

            Console.ReadKey();
        }
    }
}

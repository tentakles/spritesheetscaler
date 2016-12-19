using System;
using System.Text;
using System.IO;
namespace SpritesheetScaler
{
    public class Program
    {
        static string infile = "sprites.css";
        static string outfile = "sprites2x.css";
        static string matchingPattern = "background-position:";

        public static string ScalePixels(string line, int scalefactor)
        {
            var beginPos = line.IndexOf(matchingPattern);
            var rest = line.Substring(beginPos+ matchingPattern.Length);
            rest = rest.Replace(";","");
            rest = rest.Replace("px","");
            rest = rest.Trim();

            var values = rest.Split(' ');

            var val1 = int.Parse(values[0]) * scalefactor;
            var val2 = int.Parse(values[1])* scalefactor;

            var result = string.Format("{0} {1}px {2}px;",matchingPattern,val1,val2);
            return result;
        }

        public static void Main(string[] args)
        {
            var result = new StringBuilder();
            var lines = File.ReadAllLines(infile);

            foreach (var line in lines)
            {
                var lineresult = line;
                if (line.Contains(matchingPattern))
                    lineresult = ScalePixels(line, 2);

                result.AppendLine(lineresult);
                Console.WriteLine(lineresult);
            }
            File.WriteAllText(outfile, result.ToString());
        }
    }
}

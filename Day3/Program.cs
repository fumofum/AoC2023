// ******************************************
// ******************************************
// https://adventofcode.com/2023/day/3
// ******************************************
// ******************************************

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "input.txt";

            var linesFromFile = GetLinesFromFile(inputFilePath);

            foreach(var line in linesFromFile) 
            {

            }
        }

        static IEnumerable<string> GetLinesFromFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                var currentLine = string.Empty;
                while ((currentLine = sr.ReadLine()?.ToLower()) != null)
                {
                    yield return currentLine;
                }
            }
        }
    }
}
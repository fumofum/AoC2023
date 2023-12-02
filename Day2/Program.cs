// ******************************************
// ******************************************
// https://adventofcode.com/2023/day/2
// ******************************************
// ******************************************

namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "input.txt";

            int sumOfIds = 0;
            int sumOfPower = 0;

            using(StreamReader reader = new StreamReader(filePath))
            {
                var currentLine = string.Empty;
                while((currentLine = reader.ReadLine()?.ToLower()) != null)
                {
                    var splitedGameIdAndCubes = currentLine.Split(':');
                    string cubesSet = splitedGameIdAndCubes[1].Trim();

                    if (IsGamePossible(cubesSet))
                    {
                        if (int.TryParse(splitedGameIdAndCubes[0].Split(' ')[1].Trim(), out int id))
                            sumOfIds += id;
                    }

                    sumOfPower += CalculateCubesSetPower(cubesSet);
                }
            }

            Console.WriteLine($"Sum of IDs of possible games: {sumOfIds}");
            Console.WriteLine($"Sum of the power of cubes sets: {sumOfPower}");
        }

        static bool IsGamePossible(string cubesSet)
        {
            var cubesMaxNumbers = new Dictionary<string, int>(3)
            {
                { "red", 12 },
                { "green", 13},
                { "blue", 14}
            };

            foreach (var subset in cubesSet.Trim().Split(';'))
            {
                var cubes = subset.Trim().Split(", ");

                foreach (var color in cubesMaxNumbers.Keys)
                {
                    int numberOfCubes = GetNumberOfCubesByColor(cubes, color).FirstOrDefault();

                    if (numberOfCubes > cubesMaxNumbers[color])
                        return false;
                }
            }

            return true;
        }

        static int CalculateCubesSetPower(string cubesSet)
        {
            var cubes = cubesSet.Trim().Replace(';', ',').Split(", ");

            var colors = new List<string>(3)
            {
                "red",
                "green",
                "blue"
            };

            return colors.Select(c => GetNumberOfCubesByColor(cubes, c).Max()).Aggregate(1, (x, y) => x * y);
        }

        static IEnumerable<int> GetNumberOfCubesByColor(string[] cubes, string color)
        {
            return cubes.Where(x => x.Contains(color))
                        .Select(x => int.Parse(x.Split(' ')[0]));
        }
    }
}
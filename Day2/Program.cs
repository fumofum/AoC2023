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
            const int RED_CUBES_MAX = 12, GREEN_CUBES_MAX = 13, BLUE_CUBES_MAX = 14;

            foreach (var subset in cubesSet.Trim().Split(';'))
            {
                var cubes = subset.Trim().Split(", ");

                int redCubesNumber = GetNumberOfCubesByColor(cubes, "red").FirstOrDefault();
                int greenCubesNumber = GetNumberOfCubesByColor(cubes, "green").FirstOrDefault();
                int blueCubesNumber = GetNumberOfCubesByColor(cubes, "blue").FirstOrDefault();

                if (redCubesNumber > RED_CUBES_MAX || greenCubesNumber > GREEN_CUBES_MAX || blueCubesNumber > BLUE_CUBES_MAX)
                {
                    return false;
                }
            }

            return true;
        }

        static int CalculateCubesSetPower(string cubesSet)
        {
            var cubes = cubesSet.Trim().Replace(';', ',').Split(", ");

            int redCubesRequired = GetNumberOfCubesByColor(cubes, "red").Max();
            int greenCubesRequired = GetNumberOfCubesByColor(cubes, "green").Max();
            int blueCubesRequired = GetNumberOfCubesByColor(cubes, "blue").Max();

            return redCubesRequired * greenCubesRequired * blueCubesRequired;
        }

        static IEnumerable<int> GetNumberOfCubesByColor(string[] cubes, string color)
        {
            return cubes.Where(x => x.Contains(color))
                        .Select(x => int.Parse(x.Split(' ')[0]));
        }
    }
}
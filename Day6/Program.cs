// ******************************************
// ******************************************
// https://adventofcode.com/2023/day/6
// ******************************************
// ******************************************

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputData = File.ReadAllLines("input.txt");

            Console.WriteLine($"(Part 1) Winning ways mul:{Part1(inputData)}");
            Console.WriteLine($"(Part 2) Winning ways:{Part2(inputData)}");
        }

        static int Part1(string[] inputData)
        {
            var time = inputData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
            var distance = inputData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

            return Enumerable.Range(0, time.Length)
                             .Select(i => GetWinningWaysNum(time[i], distance[i]))
                             .Aggregate((x, y) => x * y);
        }

        static int Part2(string[] inputData)
        {
            var time = long.Parse(string.Join("", inputData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
            var distance = long.Parse(string.Join("", inputData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));

            return GetWinningWaysNum(time, distance);
        }

        static int GetWinningWaysNum(long timeLimit, long recordDistance)
        {
            long D = timeLimit * timeLimit - 4 * recordDistance;

            var x1 = (long)Math.Floor((-timeLimit + Math.Sqrt(D)) / 2);
            var x2 = (long)Math.Ceiling((-timeLimit - Math.Sqrt(D)) / 2);

            return (int)(x1 - x2 + 1);
        }
    }
}
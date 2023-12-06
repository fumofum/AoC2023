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
            var time = inputData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(uint.Parse).ToArray();
            var distance = inputData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(uint.Parse).ToArray();

            return Enumerable.Range(0, time.Length)
                             .Select(i => GetWinningWays(time[i], distance[i]).Count())
                             .Aggregate((x, y) => x * y);
        }

        static int Part2(string[] inputData)
        {
            var time = ulong.Parse(string.Join("", inputData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
            var distance = ulong.Parse(string.Join("", inputData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));

            return GetWinningWays(time, distance).Count();
        }

        static IEnumerable<uint> GetWinningWays(ulong timeLimit, ulong recordDistance)
        {
            for(uint holdingButtonTime = 1; holdingButtonTime < timeLimit - 1; ++holdingButtonTime)
            {
                if(holdingButtonTime * (timeLimit - holdingButtonTime) > recordDistance)
                    yield return holdingButtonTime;
            }
        }
    }
}
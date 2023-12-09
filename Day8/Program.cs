using System.Text.RegularExpressions;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileLines = File.ReadAllLines("input.txt").Where(x => x.Trim() != string.Empty).ToArray();

            var directions = fileLines.First().Trim().ToArray();

            var nodes = fileLines.Skip(1)
                                 .Select(x => Regex.Replace(x, "[(),=]", string.Empty).Split(" ", StringSplitOptions.RemoveEmptyEntries))
                                 .ToDictionary(x => x[0], x => (x[1], x[2]));

            Console.WriteLine($"{Part1(directions, nodes)} steps required to reach ZZZ");
            Console.WriteLine($"{Part2(directions, nodes)} steps required to reach nodes that end with Z");
        }

        static int Part1(char[] directions, Dictionary<string, (string, string)> nodes)
        {
            var currentNode = nodes.First(x => x.Key == "AAA");

            int stepsCounter;
            for (stepsCounter = 0; currentNode.Key != "ZZZ"; ++stepsCounter)
            {
                currentNode = directions[stepsCounter % directions.Count()] == 'L' ? nodes.First(x => x.Key == currentNode.Value.Item1)
                                                                                   : nodes.First(x => x.Key == currentNode.Value.Item2);
            }

            return stepsCounter;
        }

        static long Part2(char[] directions, Dictionary<string, (string, string)> nodes)
        {
            var currentNodes = nodes.Where(x => x.Key.EndsWith('A')).ToArray();
            var nodesStepsCountes = Enumerable.Repeat(0L, currentNodes.Count()).ToArray();

            for(uint i = 0; i < currentNodes.Count(); ++i)
            {
                for (nodesStepsCountes[i] = 0; !currentNodes[i].Key.EndsWith('Z'); ++nodesStepsCountes[i])
                {
                    currentNodes[i] = directions[nodesStepsCountes[i] % directions.Count()] == 'L' ? nodes.First(x => x.Key == currentNodes[i].Value.Item1)
                                                                                                   : nodes.First(x => x.Key == currentNodes[i].Value.Item2);
                }
            }

            return nodesStepsCountes.Aggregate((a, b) => a * b / gcd(a, b));
        }

        static long gcd(long a, long b)
        {
            if (a < b)
            {
                (a, b) = (b, a);
            }

            while(b > 0)
            {
                a %= b;
                (a, b) = (b, a);
            }

            return a;
        }
    }
}
// ******************************************
// ******************************************
// https://adventofcode.com/2023/day/7
// ******************************************
// ******************************************

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var allLines = File.ReadAllLines("input.txt");

            var handsData = new KeyValuePair<string, int>[allLines.Length];
            
            for(int i = 0; i < allLines.Length; ++i)
            {
                var hand = allLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                handsData[i] = new KeyValuePair<string, int>(hand[0], int.Parse(hand[1]));
            }

            var orderedHands = handsData.OrderBy(x => x.Key, new HandsComparer());

            Console.WriteLine($"Total winnings: {Enumerable.Range(1, orderedHands.Count())
                                                           .Zip(orderedHands, (x1, x2) => x1 * x2.Value)
                                                           .Sum()}");
        }
    }
}
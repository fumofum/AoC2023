// ******************************************
// ******************************************
// https://adventofcode.com/2023/day/5
// ******************************************
// ******************************************

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "input.txt";
            var allBlocks = File.ReadAllText(inputFilePath).Split("\n\n", StringSplitOptions.RemoveEmptyEntries).ToList();

            var seeds = allBlocks.First()
                                 .Split(':')[1]
                                 .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(ulong.Parse)
                                 .ToArray();

            var parsedMapBlocks = allBlocks.Skip(1)
                                           .Select(ParseMapBlock)
                                           .ToList();

            ulong locationNumber = 0;

            for(int i = 0; i < seeds.Length; i += 2)
            {
                ulong endOfRange = seeds[i] + seeds[i + 1];

                for (ulong j = seeds[i]; j < endOfRange; ++j)
                {
                    var currentValue = j;
                    foreach (var mapBlock in parsedMapBlocks)
                    {
                        currentValue = GetNextValue(currentValue, mapBlock);
                    }

                    if (locationNumber == 0 || locationNumber > currentValue)
                    {
                        locationNumber = currentValue;
                    }
                }
            }

            Console.WriteLine($"Lowest location number: {locationNumber}");
        }

        //static List<(ulong start, ulong end)> SplitRange((ulong start, ulong end) range, List<(ulong distination, ulong source, ulong length)> block)
        //{
        //    var resultRanges = new List<(ulong start, ulong end)>();

        //    if(block.FirstOrDefault(x => x.source >= range.start && range.end <= x.source + x.length) != default)
        //    {
        //        return Enumerable.Repeat(range, 1).ToList();
        //    }


        //}

        static ulong GetNextValue(ulong currentValue, List<(ulong distination, ulong source, ulong length)> block)
        {
            (ulong distination, ulong source, ulong length) line = block.Where(x => currentValue >= x.source && currentValue <= x.source + x.length - 1)
                                                                        .FirstOrDefault();

            if(line == default)
                return currentValue;

            return line.distination + (currentValue - line.source); 
        }

        static List<(ulong, ulong, ulong)> ParseMapBlock(string blockData)
        {
            var lines = blockData.Split('\n', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();

            var parsedBlock = new List<(ulong, ulong, ulong)>(lines.Count);
            foreach(var line in lines)
            {
                var splittedLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(ulong.Parse)
                                       .ToList();
                
                parsedBlock.Add((splittedLine[0], splittedLine[1], splittedLine[2]));
            }

            return parsedBlock.OrderBy(x => x.Item2).ToList();
        }
    }
}
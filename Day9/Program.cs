namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFileName = "input.txt";

            int sumOfNextExtrapolatedValues = 0;
            int sumOfPreviousExtrapolatedValues = 0;

            using (StreamReader reader = new StreamReader(inputFileName))
            {
                var currentLine = string.Empty;
                while((currentLine = reader.ReadLine()) != null)
                {
                    var currentValues = currentLine.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(x => int.Parse(x.Trim()))
                                                   .ToArray();

                    int extrapolatedValueForNext = currentValues.Last();
                    int extrapolatedValueForPrevious = currentValues.First();

                    var extrapolatedValuesAtFirstPosition = new Stack<int>();

                    while (currentValues.Any(x => x != 0))
                    {
                        currentValues = GenerateNextArrayOfValues(currentValues);
                        
                        extrapolatedValueForNext += currentValues.Last();
                        extrapolatedValuesAtFirstPosition.Push(currentValues.First());
                    }

                    sumOfNextExtrapolatedValues += extrapolatedValueForNext;
                    sumOfPreviousExtrapolatedValues += extrapolatedValueForPrevious - extrapolatedValuesAtFirstPosition.Aggregate((x, y) => y - x);
                }
            }

            Console.WriteLine($"Sum of next extrapolated values: {sumOfNextExtrapolatedValues}");
            Console.WriteLine($"Sum of previous extrapolated values: {sumOfPreviousExtrapolatedValues}");
        }

        static int[] GenerateNextArrayOfValues(int[] values)
        {
            return Enumerable.Range(1, values.Length - 1)
                             .Select(i => values[i] - values[i - 1])
                             .ToArray();
        }
    }
}
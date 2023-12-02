// ******************************************
// ******************************************
// https://adventofcode.com/2023/day/1
// ******************************************
// ******************************************

namespace Day1
{
    internal class Program
    {
        static bool TryGetSumOfCalibrationValues(string inputFilePath, bool withLetters, out int sumOfCalibrationValues)
        {
            sumOfCalibrationValues = 0;

            if(!File.Exists(inputFilePath))
                return false;

            var possibleValues = new List<string>(Enumerable.Range(0, 10).Select(x => x.ToString()));

            if (withLetters)
            {
                var digits = new List<string>()
                {
                    "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
                };

                possibleValues.AddRange(digits);
            }

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                var currentLine = string.Empty;
                while((currentLine = reader.ReadLine()?.ToLower()) != null)
                {
                    var positions = new Dictionary<int, string>();

                    possibleValues.ForEach(x =>
                    {
                        int index = 0;

                        while((index = currentLine.IndexOf(x, index)) != -1)
                        {
                            positions.Add(index, x);
                            index += x.Length;
                        }
                    });

                    if(positions.Any())
                    {
                        string firstDigitAsString = positions[positions.Keys.Min()];
                        string secondDigitAsString = positions[positions.Keys.Max()];

                        sumOfCalibrationValues += possibleValues.IndexOf(firstDigitAsString) % 10 * 10 + possibleValues.IndexOf(secondDigitAsString) % 10;
                    }
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            if (TryGetSumOfCalibrationValues("input.txt", false, out int sumOfCalibrationValuesFirstPart))
            {
                Console.WriteLine($"Sum of calibration values (first part): {sumOfCalibrationValuesFirstPart}");
            }

            if (TryGetSumOfCalibrationValues("input.txt", true, out int sumOfCalibrationValuesSecondPart))
            {
                Console.WriteLine($"Sum of calibration values (second part): {sumOfCalibrationValuesSecondPart}");
            }
        }
    }
}
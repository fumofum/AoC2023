namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "input.txt";

            int points = 0;
            
            int cardsNumber = GetLinesNumber(filePath);
            var scratchCardsCounters = Enumerable.Repeat(1, cardsNumber).ToArray();

            using (StreamReader sr = new StreamReader(filePath))
            {
                var currentLine = string.Empty;
                for(int i = 0;  (currentLine = sr.ReadLine()) != null; ++i)
                {
                    (var winningNumbers, var myNumbers) = SplitCardData(currentLine);

                    var myWinningNumbers = winningNumbers.Intersect(myNumbers);

                    if(myWinningNumbers.Any())
                    {
                        points += (int) Math.Pow(2, myWinningNumbers.Count() - 1);

                        for(int j = i + 1; j < i + myWinningNumbers.Count() + 1; ++j)
                        {
                            scratchCardsCounters[j] += scratchCardsCounters[i];
                        }
                    }
                }
            }

            Console.WriteLine($"Points: {points}");
            Console.WriteLine($"Scratchcards: {scratchCardsCounters.Sum()}");
        }
        static (List<int> winningNumbers, List<int> myNumbers) SplitCardData(string cardData)
        {
            var cardNumbers = cardData.Split(':')[1].Split('|');

            return (cardNumbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
                cardNumbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());
        }

        static int GetLinesNumber(string filePath)
            => File.ReadAllLines(filePath).Count();
    }
}
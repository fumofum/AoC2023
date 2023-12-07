namespace Day7
{
    public class HandsComparer : IComparer<string>
    {
        // sorted in strength order
        private enum HandsTypes
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfaKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }

        private Dictionary<char, int> cardStrengthMap = new Dictionary<char, int>();

        public HandsComparer()
        {
            var cardLabels = new char[] { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

            cardStrengthMap = cardLabels.Zip(Enumerable.Range(1, cardLabels.Count()), (x, y) => new { x, y })
                                        .ToDictionary(x => x.x, y => y.y);
        }

        public int Compare(string cardLabels1, string cardLabels2)
        {
            if (cardLabels1 == cardLabels2)
                return 0;

            var handType1 = GetHandType(cardLabels1);
            var handType2 = GetHandType(cardLabels2);

            if (handType1 == handType2)
            {
                (char firstHandDifLabel, char secondHandDifLabel) = cardLabels1.Zip(cardLabels2, (c1, c2) => new { c1, c2 })
                                                                               .Where(c => c.c1 != c.c2)
                                                                               .Select(c => (c.c1, c.c2))
                                                                               .FirstOrDefault();

                return cardStrengthMap[firstHandDifLabel] > cardStrengthMap[secondHandDifLabel] ? 1 : -1;
            }

            return handType1 > handType2 ? 1 : -1;
        }

        private HandsTypes GetHandType(string cardLabels)
        {
            HandsTypes handType = HandsTypes.HighCard;

            switch (JokerConvert(cardLabels).GroupBy(x => x))
            {
                case var sameCardsGroups when sameCardsGroups.Count() == 1:
                    handType = HandsTypes.FiveOfAKind;
                    break;
                case var sameCardsGroups when sameCardsGroups.Count() == 2 && sameCardsGroups.Where(x => x.Count() == 4).Any():
                    handType = HandsTypes.FourOfAKind;
                    break;
                case var sameCardsGroups when sameCardsGroups.Count() == 2:
                    handType = HandsTypes.FullHouse;
                    break;
                case var sameCardsGroups when sameCardsGroups.Count() == 3 && sameCardsGroups.Where(x => x.Count() == 1).Count() == 2:
                    handType = HandsTypes.ThreeOfaKind;
                    break;
                case var sameCardsGroups when sameCardsGroups.Count() == 3:
                    handType = HandsTypes.TwoPair;
                    break;
                case var sameCardsGroups when sameCardsGroups.Count() == 4:
                    handType = HandsTypes.OnePair;
                    break;
            }

            return handType;
        }

        private string JokerConvert(string cardLabels)
        {
            if (!cardLabels.Contains('J'))
                return cardLabels;

            char mostRepeatedChar = cardLabels.Where(x => x != 'J')
                                              .GroupBy(x => x)
                                              .MaxBy(x => x.Count())?.Key ?? 'J';

            return cardLabels.Replace('J', mostRepeatedChar);
        }
    }
}

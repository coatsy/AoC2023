<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day07";
	// var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	var inputFile = @"input.txt";

	var Hands = new List<Hand>();

	using (var file = File.OpenText(Path.Combine(baseFolder, dayFolder, inputFile)))
	{
		while (!file.EndOfStream)
		{
			Hands.Add(new Hand(await file.ReadLineAsync()));
		}
	}

	// Hands.Dump();

	// sort and index the list

	var sortedHands = Hands.OrderBy(h => h, new HandComparer()).Select((h, index) => new RankedHand { Hand = h, Strength = index + 1 });

	//sortedHands.Dump();

	sortedHands.Sum(sh => sh.Winnings).Dump("Total Winnings - Part 1");

	var sortedHandsWithJokers = Hands.OrderBy(h => h, new HandWithJokersComparer()).Select((h, index) => new RankedHand { Hand = h, Strength = index + 1 });
	sortedHandsWithJokers.Sum(sh => sh.Winnings).Dump("Total Winnings With Jokers - Part 2");

}

// You can define other methods, fields, classes and namespaces here

public class RankedHand
{
	public Hand Hand;
	public int Strength;

	public int Winnings => Hand.Bid * Strength;
}
public class Hand
{
	public string Cards;
	public int Bid;

	public Hand(string handString)
	{
		var components = handString.Trim().Split(' ');
		Cards = components[0];
		Bid = int.Parse(components[1]);
	}

	public HandType HandTypeWithJokers
	{
		get
		{
			HandType handType = HandType.Unknown;
			var jokers = Cards.Count(c => c == 'J');

			if (jokers >= 4)
			{
				return HandType.FiveOfAKind;
			}

			var newCardCount = Cards.Replace("J", "").GroupBy(ca => ca).Select(ca => new { Card = ca, Count = ca.Count() });

			if (jokers == 3)
			{
				if (newCardCount.Count() == 1)
				{
					return HandType.FiveOfAKind;
				}
				else
				{
					return HandType.FourOfAKind;
				}
			}

			if (jokers == 2)
			{
				if (newCardCount.Count() == 1)
				{
					return HandType.FiveOfAKind;
				}
				else if (newCardCount.Count() == 2)
				{
					return HandType.FourOfAKind;
				}
				else
				{
					return HandType.ThreeOfAKind;
				}
			}
			
			if (jokers == 1)
			{
				if (newCardCount.Count() == 1)
				{
					return HandType.FiveOfAKind;
				}
				else if (newCardCount.Count() == 2)
				{
					if (newCardCount.Max(cc => cc.Count) == 3)
					{
						return HandType.FourOfAKind;
					} else {
						return HandType.FullHouse;
					}
				}
				else if (newCardCount.Count() == 3)
				{
					return HandType.ThreeOfAKind;
				}
				else
				{
					return HandType.OnePair;
				}
			}

			switch (newCardCount.Count())
			{
				case 1:
					return UserQuery.HandType.FiveOfAKind;
					break;
				case 5:
					return UserQuery.HandType.HighCard;
					break;
				case 4:
					return UserQuery.HandType.OnePair;
					break;
				case 2:
					if (newCardCount.Max(c => c.Count == 4))
						return UserQuery.HandType.FourOfAKind;
					else
						return UserQuery.HandType.FullHouse;
					break;
				case 3:
					if (newCardCount.Max(c => c.Count == 3))
						return UserQuery.HandType.ThreeOfAKind;
					else
						return UserQuery.HandType.TwoPair;
					break;
				default:
					break;
			}
			return handType;
		}
	}

	public HandType HandType
	{
		get
		{
			HandType handType = HandType.Unknown;
			// get the distinct cards and their count
			var cardCount = Cards.GroupBy(ca => ca).Select(ca => new { Card = ca.Key, Count = ca.Count() });

			switch (cardCount.Count())
			{
				case 1:
					handType = UserQuery.HandType.FiveOfAKind;
					break;
				case 5:
					handType = UserQuery.HandType.HighCard;
					break;
				case 4:
					handType = UserQuery.HandType.OnePair;
					break;
				case 2:
					if (cardCount.Max(c => c.Count == 4))
						handType = UserQuery.HandType.FourOfAKind;
					else
						handType = UserQuery.HandType.FullHouse;
					break;
				case 3:
					if (cardCount.Max(c => c.Count == 3))
						handType = UserQuery.HandType.ThreeOfAKind;
					else
						handType = UserQuery.HandType.TwoPair;
					break;
				default:
					break;
			}
			return handType;
		}
	}

}

public enum HandType
{
	Unknown = 0,
	HighCard = 1,
	OnePair = 2,
	TwoPair = 3,
	ThreeOfAKind = 4,
	FullHouse = 5,
	FourOfAKind = 6,
	FiveOfAKind = 7
}

public class HandComparer : IComparer<Hand>
{
	private const string cardOrder = "23456789TJQKA";
	public int Compare(Hand x, Hand y)
	{
		if (x.HandType < y.HandType)
			return -1;
		else if (x.HandType > y.HandType)
			return 1;
		else
		{
			for (int i = 0; i < x.Cards.Length; i++)
			{
				if (cardOrder.IndexOf(x.Cards[i]) < cardOrder.IndexOf(y.Cards[i]))
					return -1;
				else if (cardOrder.IndexOf(x.Cards[i]) > cardOrder.IndexOf(y.Cards[i]))
					return 1;
			}
			return 0;
		}
	}
}

public class HandWithJokersComparer : IComparer<Hand>
{
	private const string cardOrder = "J23456789TQKA";
	public int Compare(Hand x, Hand y)
	{
		if (x.HandTypeWithJokers < y.HandTypeWithJokers)
			return -1;
		else if (x.HandTypeWithJokers > y.HandTypeWithJokers)
			return 1;
		else
		{
			for (int i = 0; i < x.Cards.Length; i++)
			{
				if (cardOrder.IndexOf(x.Cards[i]) < cardOrder.IndexOf(y.Cards[i]))
					return -1;
				else if (cardOrder.IndexOf(x.Cards[i]) > cardOrder.IndexOf(y.Cards[i]))
					return 1;
			}
			return 0;
		}
	}
}



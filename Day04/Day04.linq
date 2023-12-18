<Query Kind="Program" />

void Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day04";
	//var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	var inputFile = @"input.txt";

	var contents = File.ReadLines(Path.Join(baseFolder, dayFolder, inputFile)).ToArray();

	var Cards = contents.Select(c => new Card(c)).ToList();

	Cards.Sum(c => c.Points).Dump("Total Points");

	// Now process Part 2's method of getting new cards for matches
	while (Cards.Any(c=>!c.Processed))
	{
		foreach (var card in Cards.Where(c=> !c.Processed).ToList())
		{
			card.Processed = true;
			for (int no = card.CardNo+1; no < card.CardNo + card.Matches + 1; no++)
			{
				Cards.Add(Cards.First(c=>c.CardNo == no).GetCopy());
			}
		}
	}
	
	Cards.Count.Dump("Total Cards");

}

// You can define other methods, fields, classes and namespaces here

class Card
{
	public int CardNo;
	public List<int> Winners;
	public List<int> MyNumbers;
	public int Matches => MyNumbers.Count(n => Winners.Contains(n));
	public Int64 Points => Matches == 0 ? 0 : (Int64)System.Math.Pow(2, Matches - 1);
	public bool Processed = false;

	public Card GetCopy()
	{
		return new Card
		{
			CardNo = this.CardNo,
			Processed = false,
			MyNumbers = this.MyNumbers,
			Winners = this.Winners
		};

	}

	public Card() { }

	public Card(string cardLine)
	{
		var components = cardLine.Split(": ");
		CardNo = int.Parse(components[0].Substring("Card ".Length));
		var numbers = components[1].Split(" | ");
		Winners = numbers[0].Replace("  ", " ").Trim().Split(' ').Select(s => int.Parse(s)).ToList();
		MyNumbers = numbers[1].Replace("  ", " ").Trim().Split(' ').Select(s => int.Parse(s)).ToList();
	}
}
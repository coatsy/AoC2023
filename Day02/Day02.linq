<Query Kind="Program" />

void Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day02";
	// var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	var inputFile = @"input.txt";

	var contents = File.ReadLines(Path.Join(baseFolder, dayFolder, inputFile));
	
	var Games = new List<Game>();
	
	foreach (var c in contents)
	{
		Games.Add(ReadGame(c));
	}

	Games.Where(g=>g.Possible).Sum(g=>g.Id).Dump();
	Games.Dump();
	Games.Sum(g=>g.Power).Dump();
	
}

// You can define other methods, fields, classes and namespaces here

Game ReadGame(string gameLine)
{
	var game = new Game();
	
	var components = gameLine.Split(':');
	game.Id = int.Parse(components[0].Substring("Game ".Length));
	game.Sets = readSets(components[1].ToLowerInvariant());
	
	return game;
}

List<Set> readSets(string setString)
{
	var sets = new List<Set>();
	var setStrings = setString.Split(';');
	
	foreach (var s in setStrings)
	{
		var set = new Set {Red = 0, Green = 0, Blue = 0};
		var components = s.Split(',');
		foreach (var c in components)
		{
			if (c.Substring(c.Length - 3,3) == "red")
			{
				set.Red = int.Parse(c.Substring(0, c.Length - 4));
			}
			if (c.Substring(c.Length - 5, 5) == "green")
			{
				set.Green = int.Parse(c.Substring(0, c.Length - 6));
			}
			if (c.Substring(c.Length - 4, 4) == "blue")
			{
				set.Blue = int.Parse(c.Substring(0, c.Length - 5));
			}
		}
		sets.Add(set);
	}
	return sets;
}

class Set
{
	public int Red;
	public int Green;
	public int Blue;
}

class Game
{
	private const int maxRed = 12;
	private const int maxGreen = 13;
	private const int maxBlue = 14;
	public int Id;
	public List<Set> Sets;
	public bool Possible => !(Sets.Any(s => s.Red > maxRed || s.Blue > maxBlue || s.Green > maxGreen));
	public Set Fewest => new Set {Red = Sets.Max(s=> s.Red), Green = Sets.Max(s => s.Green), Blue = Sets.Max(s => s.Blue)};
	public Int64 Power => Fewest.Red * Fewest.Green * Fewest.Blue;
}
<Query Kind="Program" />

void Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day06";
	//var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	var inputFile = @"input.txt";

	var contents = File.ReadLines(Path.Join(baseFolder, dayFolder, inputFile)).ToArray();
	var timestr = contents[0].Substring("Time: ".Length).Trim();
	while (timestr.Contains("  "))
	{
		timestr = timestr.Replace("  ", " ");
	}
	
	var times = timestr.Split(' ');
	
	var distancestr = contents[1].Substring("Distance: ".Length).Trim();
	while (distancestr.Contains("  "))
	{
		distancestr = distancestr.Replace("  ", " ");
	}
	var distances = distancestr.Split(' ');

	var Races = new List<Race>();
	
	for (int i = 0; i < times.Length; i++)
	{
		Races.Add(new Race {Time = long.Parse(times[i]), Distance = long.Parse(distances[i])});
	}
	
	Races.Dump("Races");
	long product = 1;
	
	foreach (var race in Races)
	{
		product *= race.Ways2Win;
	}
	
	product.Dump("Product of ways to win - Part 1");

	var NewRace = new Race {Time = long.Parse(timestr.Replace(" ", "")), Distance = long.Parse(distancestr.Replace(" ", ""))}; 
	NewRace.Dump("New Race");
}

// You can define other methods, fields, classes and namespaces here

public class Race
{
	public long Time;
	public long Distance;
	
	public int Ways2Win
	{
		get
		{
			var sq = Math.Sqrt((double)(Time * Time - 4*Distance));
			var lower = ((double)Time - sq)/2;
			// deal with the root being an integer - just matching the time's not good enough
			lower += (lower == (int)lower ? 1d : 0d);
			var upper = ((double)Time + sq)/2;
			upper -= (upper == (int)upper? 1d : 0d);
			return (int)(Math.Floor(upper) - Math.Ceiling(lower) + 1);
		}
	}
}
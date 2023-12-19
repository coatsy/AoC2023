<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day05";
	//var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	var inputFile = @"input.txt";

	List<long> seeds = null;
	var seeds2soil = new List<Map>();
	var soil2fert = new List<Map>();
	var fert2water = new List<Map>();
	var water2light = new List<Map>();
	var light2temp = new List<Map>();
	var temp2humid = new List<Map>();
	var humid2loc = new List<Map>();

	List<Map> thisMap = null;

	using (var file = File.OpenText(Path.Combine(baseFolder, dayFolder, inputFile)))
	{
		var state = State.seeds;
		while (!file.EndOfStream)
		{
			var thisLine = file.ReadLine();

			if (string.IsNullOrWhiteSpace(thisLine))
			{
				state = State.unknown;
				continue;
			}

			if (state == State.seeds)
			{
				var components = thisLine.Split(": ");
				seeds = components[1].Split(' ').Select(m => long.Parse(m)).ToList();
				continue;
			}

			if (thisLine.Contains(':'))
			{
				switch (thisLine.Substring(0, thisLine.IndexOf('-')))
				{
					case "seed":
						state = State.seeds2soil;
						thisMap = seeds2soil;
						break;
					case "soil":
						state = State.soil2fert;
						thisMap = soil2fert;
						break;
					case "fertilizer":
						state = State.fert2water;
						thisMap = fert2water;
						break;
					case "water":
						state = State.water2light;
						thisMap = water2light;
						break;
					case "light":
						state = State.light2temp;
						thisMap = light2temp;
						break;
					case "temperature":
						state = State.temp2humid;
						thisMap = temp2humid;
						break;
					case "humidity":
						state = State.humid2loc;
						thisMap = humid2loc;
						break;
					default:
						state = State.unknown;
						thisMap = null;
						break;
				}

				continue;
			}

			thisMap.Add(new Map(thisLine));

		}
	}

	//seeds.Dump("seeds");
	//seeds2soil.Dump("seed-to-soil");

	var locations = seeds.Select(s =>
	humid2loc.GetDest(
		temp2humid.GetDest(
			light2temp.GetDest(
				water2light.GetDest(
					fert2water.GetDest(
						soil2fert.GetDest(
							seeds2soil.GetDest(s))))))));

	locations.Min().Dump("Minimum Location - Part 1");

	seeds.Max().Dump("Max Seeds");
	var SeedRanges = new List<SeedRange>();
	for (int i = 0; i < seeds.Count; i += 2)
	{
		SeedRanges.Add(new SeedRange { Start = seeds[i], Len = seeds[i + 1] });
	}

	SeedRanges.Dump("Seed Ranges");

	var SeedLocations = new List<SeedLoc>();

	foreach (var sr in SeedRanges)
	{
		sr.Dump("Processing...");
		for (long seed = sr.Start; seed < sr.Start + sr.Len; seed++)
		{

			//}
			//Parallel.For(sr.Start, sr.Start + sr.Len, seed =>
			//{
			SeedLocations.Add(new SeedLoc
			{
				Seed = seed,
				Location = humid2loc.GetDest(
					temp2humid.GetDest(
						light2temp.GetDest(
							water2light.GetDest(
								fert2water.GetDest(
									soil2fert.GetDest(
										seeds2soil.GetDest(seed)))))))
			});
			//});
		}
	}

	SeedLocations.Count.Dump("Total Seeds");
	SeedLocations.Min(sl => sl.Location).Dump("Minimum Location - Part 2");
}

// You can define other methods, fields, classes and namespaces here
public class Map
{
	public long DestStart;
	public long SourceStart;
	public long Len;
	public Map(string mapString)
	{
		var components = mapString.Split(' ');
		DestStart = long.Parse(components[0]);
		SourceStart = long.Parse(components[1]);
		Len = long.Parse(components[2]);
	}
}

public enum State
{
	unknown,
	seeds,
	seeds2soil,
	soil2fert,
	fert2water,
	water2light,
	light2temp,
	temp2humid,
	humid2loc
}

public struct SeedLoc
{
	public long Seed;
	public long Location;
}

public static class ExtensionMethods
{
	public static long GetDest(this List<Map> maps, long source)
	{
		var matchingMap = maps.FirstOrDefault(m => source >= m.SourceStart && source <= m.SourceStart + m.Len);
		if (matchingMap == null)
		{
			return source;
		}
		else
		{
			return matchingMap.DestStart + source - matchingMap.SourceStart;
		}
	}
}

public class SeedRange
{
	public long Start;
	public long Len;
}
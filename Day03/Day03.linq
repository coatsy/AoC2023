<Query Kind="Program" />

void Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day03";
	//var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	var inputFile = @"input.txt";

	var contents = File.ReadLines(Path.Join(baseFolder, dayFolder, inputFile)).ToArray();

	var partNumbers = new List<int>();

	for (int l = 0; l < contents.Length; l++)
	{
		var line = contents[l];
		var chars = new List<char>();
		int firstnum = 0;
		bool firstFound = false;

		for (int i = 0; i < line.Length; i++)
		{
			if (char.IsDigit(line[i]))
			{
				chars.Add(line[i]);
				// register where on this line 
				if (!firstFound)
				{
					firstFound = true;
					firstnum = i;
				}
			}

			// if this character is not a symbol, or if we're in a number and this is the last character in the line
			if ((firstFound && !char.IsDigit(line[i])) || (firstFound && i == line.Length - 1))
			{
				firstFound = false;
				if (chars.Count > 0)
				{
					bool foundSymbol = false;
					// check the rectangle around the number for symbols
					// if we're not on the first line, check above
					if (l > 0)
					{
						for (int p = int.Max(0, firstnum - 1); p < int.Min(firstnum + chars.Count + 1, contents[l - 1].Length); p++)
						{
							if (contents[l - 1][p].IsSymbol())
							{
								foundSymbol = true;
								break;
							}
						}
					}

					// check the character before for a symbol
					if ((!foundSymbol) && firstnum > 0)
					{
						if (line[firstnum - 1].IsSymbol())
						{
							foundSymbol = true;
						}
					}

					// check the character after for a symbol
					if ((!foundSymbol) && (firstnum + chars.Count) < line.Length)
					{
						if (line[firstnum + chars.Count].IsSymbol())
						{
							foundSymbol = true;
						}
					}

					// if we're not on the last line, check below
					if ((!foundSymbol) && l < contents.Length - 1)
					{
						for (int p = int.Max(0, firstnum - 1); p < int.Min(firstnum + chars.Count + 1, contents[l + 1].Length); p++)
						{
							if (contents[l + 1][p].IsSymbol())
							{
								foundSymbol = true;
								break;
							}
						}
					}

					// if this number has an adjacent symbol
					// convert the number to an int and add it to our list of numbers
					if (foundSymbol)
					{
						partNumbers.Add(int.Parse(new String(chars.ToArray())));
					}
					// reset our temp things
					chars.Clear();
					firstFound = false;
					firstnum = 0;
				}
			}
		}
	}

	// partNumbers.Dump();
	partNumbers.Sum().Dump("Sum of the part numbers");

	// now loop through and find all the *
	var Stars = new List<StarLoc>();
	for (int l = 0; l < contents.Length; l++)
	{
		var line = contents[l];
		for (int i = 0; i < line.Length; i++)
		{
			if (line[i] != '*')
			{
				continue;
			}

			// we've found a * - are there numbers around it?
			var surrounds = new bool[3, 3];

			// not the first row
			if (l > 0)
			{
				for (int c = int.Max(0, i - 1); c < int.Min(i + 2, line.Length); c++)
				{
					surrounds[0, c - (i - 1)] = char.IsDigit(contents[l - 1][c]);
				}
			}

			// not the first column
			if (i > 0)
			{
				surrounds[1, 0] = char.IsDigit(line[i - 1]);
			}

			// not the last column
			if (i < line.Length - 1)
			{
				surrounds[1, 2] = char.IsDigit(line[i + 1]);
			}

			// not the last row
			if (l < contents.Length - 1)
			{
				for (int c = int.Max(0, i - 1); c < int.Min(i + 2, line.Length); c++)
				{
					surrounds[2, c - (i - 1)] = char.IsDigit(contents[l + 1][c]);
				}
			}

			Stars.Add(new StarLoc { Row = l, Col = i, Surrounds = surrounds });
		}
	}


	foreach (var s in Stars)
	{
		// work across and then down matrix.
		// row above star
		var processed = new bool[3, 3];
		processed[1, 1] = true;

		processed[0, 0] = true;
		if (s.Surrounds[0, 0])
		{
			var startcol = s.Col - 1;
			while (startcol > 0 && char.IsDigit(contents[s.Row - 1][startcol - 1]))
			{
				startcol--;
			}
			// found the start column, now get the number
			var chars = new List<char>();
			while (startcol < contents[s.Row - 1].Length && char.IsDigit(contents[s.Row - 1][startcol]))
			{
				if (startcol >= s.Col && startcol - s.Col + 1 <= 2)
				{
					processed[0, startcol - s.Col + 1] = true;
				}
				chars.Add(contents[s.Row - 1][startcol]);
				startcol++;
			}
			s.Num1 = int.Parse(new String(chars.ToArray()));
		}


		if (!processed[0, 1])
		{
			processed[0, 1] = true;
			if (s.Surrounds[0, 1])
			{
				var startcol = s.Col;
				// found the start column, now get the number
				var chars = new List<char>();
				while (startcol < contents[s.Row - 1].Length && char.IsDigit(contents[s.Row - 1][startcol]))
				{
					if (startcol >= s.Col && startcol - s.Col + 1 <= 2)
					{
						processed[0, startcol - s.Col + 1] = true;
					}
					chars.Add(contents[s.Row - 1][startcol]);
					startcol++;
				}
				s.Num1 = int.Parse(new String(chars.ToArray()));
			}
		}

		if (!processed[0, 2])
		{
			processed[0, 2] = true;
			if (s.Surrounds[0, 2])
			{
				var startcol = s.Col + 1;
				// found the start column, now get the number
				var chars = new List<char>();
				while (startcol < contents[s.Row - 1].Length && char.IsDigit(contents[s.Row - 1][startcol]))
				{
					if (startcol >= s.Col && startcol - s.Col + 1 <= 2)
					{
						processed[0, startcol - s.Col + 1] = true;
					}
					chars.Add(contents[s.Row - 1][startcol]);
					startcol++;
				}
				if (s.Num1 == 0)
				{
					s.Num1 = int.Parse(new String(chars.ToArray()));
				}
				else
				{
					s.Num2 = int.Parse(new String(chars.ToArray()));
				}
			}
		}

		// row of star
		processed[1, 0] = true;
		if (s.Surrounds[1, 0])
		{
			var startcol = s.Col - 1;
			while (startcol > 0 && char.IsDigit(contents[s.Row][startcol - 1]))
			{
				startcol--;
			}
			// found the start column, now get the number
			var chars = new List<char>();
			while (startcol < contents[s.Row].Length && char.IsDigit(contents[s.Row][startcol]))
			{
				chars.Add(contents[s.Row][startcol]);
				startcol++;
			}
			if (s.Num1 == 0)
			{
				s.Num1 = int.Parse(new String(chars.ToArray()));
			}
			else
			{
				s.Num2 = int.Parse(new String(chars.ToArray()));
			}
		}

		processed[1, 2] = true;
		if (s.Surrounds[1, 2])
		{
			var startcol = s.Col + 1;
			// found the start column, now get the number
			var chars = new List<char>();
			while (startcol < contents[s.Row].Length && char.IsDigit(contents[s.Row][startcol]))
			{
				chars.Add(contents[s.Row][startcol]);
				startcol++;
			}
			if (s.Num1 == 0)
			{
				s.Num1 = int.Parse(new String(chars.ToArray()));
			}
			else
			{
				s.Num2 = int.Parse(new String(chars.ToArray()));
			}
		}

		// row below star
		processed[2, 0] = true;
		if (s.Surrounds[2, 0])
		{
			var startcol = s.Col - 1;
			while (startcol > 0 && char.IsDigit(contents[s.Row + 1][startcol - 1]))
			{
				startcol--;
			}
			// found the start column, now get the number
			var chars = new List<char>();
			while (startcol < contents[s.Row + 1].Length && char.IsDigit(contents[s.Row + 1][startcol]))
			{
				if (startcol >= s.Col && startcol - s.Col + 1 <= 2)
				{
					processed[2, startcol - s.Col + 1] = true;
				}
				chars.Add(contents[s.Row + 1][startcol]);
				startcol++;
			}
			if (s.Num1 == 0)
			{
				s.Num1 = int.Parse(new String(chars.ToArray()));
			}
			else
			{
				s.Num2 = int.Parse(new String(chars.ToArray()));
			}
		}


		if (!processed[2, 1])
		{
			processed[2, 1] = true;
			if (s.Surrounds[2, 1])
			{
				var startcol = s.Col;
				// found the start column, now get the number
				var chars = new List<char>();
				while (startcol < contents[s.Row + 1].Length && char.IsDigit(contents[s.Row + 1][startcol]))
				{
					if (startcol >= s.Col && startcol - s.Col + 1 <= 2)
					{
						processed[2, startcol - s.Col + 1] = true;
					}
					chars.Add(contents[s.Row + 1][startcol]);
					startcol++;
				}
				if (s.Num1 == 0)
				{
					s.Num1 = int.Parse(new String(chars.ToArray()));
				}
				else
				{
					s.Num2 = int.Parse(new String(chars.ToArray()));
				}
			}
		}

		if (!processed[2, 2])
		{
			processed[2, 2] = true;
			if (s.Surrounds[2, 2])
			{
				var startcol = s.Col + 1;
				// found the start column, now get the number
				var chars = new List<char>();
				while (startcol < contents[s.Row + 1].Length && char.IsDigit(contents[s.Row + 1][startcol]))
				{
					if (startcol >= s.Col && startcol - s.Col + 1 <= 2)
					{
						processed[2, startcol - s.Col + 1] = true;
					}
					chars.Add(contents[s.Row + 1][startcol]);
					startcol++;
				}
				if (s.Num1 == 0)
				{
					s.Num1 = int.Parse(new String(chars.ToArray()));
				}
				else
				{
					s.Num2 = int.Parse(new String(chars.ToArray()));
				}
			}
		}


	}


	//Stars.Dump();
	Stars.Sum(s=>s.ratio).Dump("Sum of the Gear Ratios");

}

// You can define other methods, fields, classes and namespaces here

class StarLoc
{
	public int Row;
	public int Col;
	public bool[,] Surrounds;
	public int Num1 = 0;
	public int Num2 = 0;
	public Int64 ratio => Num1 * Num2;

}

public static class ExtensionMethods
{
	public static bool IsSymbol(this char c)
	{
		return !(char.IsDigit(c) || c == '.');
	}
}
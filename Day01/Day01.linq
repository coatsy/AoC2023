<Query Kind="Program" />

void Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day01";
	//var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	var inputFile = @"input.txt";
	
//	var errorteststring = "threerzcxhlvdjkhtlxlg6ninetwonine1";

	var contents = File.ReadLines(Path.Join(baseFolder, dayFolder, inputFile));
	var digitList = new List<digits>();
	
// 	var testdig = new digits(errorteststring);
	

	foreach (var line in contents)
	{
		var dig = new digits(line);

		//line.Dump();
		//dig.result.Dump();
		digitList.Add(dig);
	}

	// digitList.Dump();

	digitList.Sum(d => d.result).Dump();

}

// You can define other methods, fields, classes and namespaces here
class digits
{
	public int First;
	public int Last;

	public int result => int.Parse($"{First}{Last}");
	
	public digits(string line)
	{
		var pos = 0;
		foreach (var l in line.ToLowerInvariant())
		{
			if (char.IsDigit(l))
			{
				First = int.Parse(l.ToString());
				break;
			}

			if (l == 'z'
				&& pos < line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'e'
				&& line.ToLowerInvariant()[pos + 2] == 'r'
				&& line.ToLowerInvariant()[pos + 3] == 'o')
			{
				First = 0;
				break;
			}

			if (l == 'o'
				&& pos < line.Length - 3
				&& line.ToLowerInvariant()[pos + 1] == 'n'
				&& line.ToLowerInvariant()[pos + 2] == 'e')
			{
				First = 1;
				break;
			}

			if (l == 't'
				&& pos < line.Length - 3
				&& line.ToLowerInvariant()[pos + 1] == 'w'
				&& line.ToLowerInvariant()[pos + 2] == 'o')
			{
				First = 2;
				break;
			}

			if (l == 't'
				&& pos < line.Length - 5
				&& line.ToLowerInvariant()[pos + 1] == 'h'
				&& line.ToLowerInvariant()[pos + 2] == 'r'
				&& line.ToLowerInvariant()[pos + 3] == 'e'
				&& line.ToLowerInvariant()[pos + 4] == 'e')
			{
				First = 3;
				break;
			}

			if (l == 'f'
				&& pos < line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'o'
				&& line.ToLowerInvariant()[pos + 2] == 'u'
				&& line.ToLowerInvariant()[pos + 3] == 'r')
			{
				First = 4;
				break;
			}

			if (l == 'f'
				&& pos < line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'v'
				&& line.ToLowerInvariant()[pos + 3] == 'e')
			{
				First = 5;
				break;
			}

			if (l == 's'
				&& pos < line.Length - 3
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'x')
			{
				First = 6;
				break;
			}

			if (l == 's'
				&& pos < line.Length - 5
				&& line.ToLowerInvariant()[pos + 1] == 'e'
				&& line.ToLowerInvariant()[pos + 2] == 'v'
				&& line.ToLowerInvariant()[pos + 3] == 'e'
				&& line.ToLowerInvariant()[pos + 4] == 'n')
			{
				First = 7;
				break;
			}

			if (l == 'e'
				&& pos < line.Length - 5
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'g'
				&& line.ToLowerInvariant()[pos + 3] == 'h'
				&& line.ToLowerInvariant()[pos + 4] == 't')
			{
				First = 8;
				break;
			}

			if (l == 'n'
				&& pos < line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'n'
				&& line.ToLowerInvariant()[pos + 3] == 'e')
			{
				First = 9;
				break;
			}

			pos++;
		}

		pos = line.Length;
		foreach (var l in line.ToLowerInvariant().Reverse())
		{
			pos--;
			if (char.IsDigit(l))
			{
				Last = int.Parse(l.ToString());
				break;
			}

			if (l == 'z'
				&& pos <= line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'e'
				&& line.ToLowerInvariant()[pos + 2] == 'r'
				&& line.ToLowerInvariant()[pos + 3] == 'o')
			{
				Last = 0;
				break;
			}

			if (l == 'o'
				&& pos <= line.Length - 3
				&& line.ToLowerInvariant()[pos + 1] == 'n'
				&& line.ToLowerInvariant()[pos + 2] == 'e')
			{
				Last = 1;
				break;
			}

			if (l == 't'
				&& pos <= line.Length - 3
				&& line.ToLowerInvariant()[pos + 1] == 'w'
				&& line.ToLowerInvariant()[pos + 2] == 'o')
			{
				Last = 2;
				break;
			}

			if (l == 't'
				&& pos <= line.Length - 5
				&& line.ToLowerInvariant()[pos + 1] == 'h'
				&& line.ToLowerInvariant()[pos + 2] == 'r'
				&& line.ToLowerInvariant()[pos + 3] == 'e'
				&& line.ToLowerInvariant()[pos + 4] == 'e')
			{
				Last = 3;
				break;
			}

			if (l == 'f'
				&& pos <= line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'o'
				&& line.ToLowerInvariant()[pos + 2] == 'u'
				&& line.ToLowerInvariant()[pos + 3] == 'r')
			{
				Last = 4;
				break;
			}

			if (l == 'f'
				&& pos <= line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'v'
				&& line.ToLowerInvariant()[pos + 3] == 'e')
			{
				Last = 5;
				break;
			}

			if (l == 's'
				&& pos <= line.Length - 3
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'x')
			{
				Last = 6;
				break;
			}

			if (l == 's'
				&& pos <= line.Length - 5
				&& line.ToLowerInvariant()[pos + 1] == 'e'
				&& line.ToLowerInvariant()[pos + 2] == 'v'
				&& line.ToLowerInvariant()[pos + 3] == 'e'
				&& line.ToLowerInvariant()[pos + 4] == 'n')
			{
				Last = 7;
				break;
			}

			if (l == 'e'
				&& pos <= line.Length - 5
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'g'
				&& line.ToLowerInvariant()[pos + 3] == 'h'
				&& line.ToLowerInvariant()[pos + 4] == 't')
			{
				Last = 8;
				break;
			}

			if (l == 'n'
				&& pos <= line.Length - 4
				&& line.ToLowerInvariant()[pos + 1] == 'i'
				&& line.ToLowerInvariant()[pos + 2] == 'n'
				&& line.ToLowerInvariant()[pos + 3] == 'e')
			{
				Last = 9;
				break;
			}

		}

	}
}
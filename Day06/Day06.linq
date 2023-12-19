<Query Kind="Program" />

void Main()
{
	var baseFolder = @"C:\Users\coats\source\repos\coatsy\AoC2023";
	var dayFolder = @"Day06";
	var inputFile = @"test.txt";
	//var inputFile = @"test2.txt";
	//var inputFile = @"input.txt";

	var contents = File.ReadLines(Path.Join(baseFolder, dayFolder, inputFile)).ToArray();

}

// You can define other methods, fields, classes and namespaces here

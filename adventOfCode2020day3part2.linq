<Query Kind="Program" />

char tree = '#';
char empty = '.';
void Main()
{

	var test = getSlopesToTest();
	long value = 1;
	foreach (var tes in test)
	{
		value *= getTreesForSlope(tes.Item1, tes.Item2);
	}

	value.Dump();

}

private List<(int, int)> getSlopesToTest()
{
	var list = new List<(int, int)>();
	list.Add((1, 1));
	list.Add((3, 1));
	list.Add((5, 1));
	list.Add((7, 1));
	list.Add((1, 2));


	return list;
}

private long getTreesForSlope(int xStep, int yStep)
{
	var inputList = new List<string>();
	inputList = ReadMyFile(@"d:\input3.txt");
	int x = 0;
	int y = 0;
	long trees = 0;
	while (y < inputList.Count-1)
	{

		for (int ii = 0; ii < xStep; ii++)
		{
			x++;
			if (x == inputList[y].Length)
				x = 0;
		}
			y += yStep;
		if (inputList[y][x] == tree)
		{
			trees++;
			//($"found tree at x{x},y{y}").Dump();
		}
		//printMap(inputList[y], x);
	

	}


	"trees".Dump();
	trees.Dump();
	return trees;
}

private void printMap(string line, int x)
{
	string currentLine = "";
	int xx = 1;
	foreach (var position in line.ToCharArray())
	{
		if (xx == x)
		{
			if (position == empty)
				currentLine += "0";
			else if (position == tree)
				currentLine += "X";
			else
				currentLine += position;

		}
		else

			currentLine += position;
		xx++;
	}

	currentLine.Dump();
}


private List<string> ReadMyFile(string uri)
{
	var newList = new List<string>();
	string line;
	System.IO.StreamReader reader = new StreamReader(uri);
	while ((line = reader.ReadLine()) != null)
	{
		newList.Add(line);
	}

	return newList;
}




// Define other methods and classes here
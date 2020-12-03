<Query Kind="Program" />

char tree = '#';
char empty = '.';
void Main()
{
	var inputList = new List<string>();
	inputList = ReadMyFile(@"d:\input3.txt");
	int x = 0;
	int y = 1;
	int trees = 0;
	while (y < inputList.Count)
	{

		for (int ii = 0; ii < 3; ii++)
		{
			x++;
			if (x == inputList[y].Length)
				x = 0;
		}
		if (inputList[y][x] == tree)
		{
			trees++;
			//($"found tree at x{x},y{y}").Dump();
		}
		printMap(inputList[y], x);
		y++;

	}


	"trees".Dump();
	trees.Dump();

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
				currentLine+=position;
				
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
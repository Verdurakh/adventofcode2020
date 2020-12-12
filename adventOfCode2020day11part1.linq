<Query Kind="Program" />

const char EMPTY = 'L';
const char OCCUPIED = '#';
const char FLOOR = '.';
bool live = true;
int iterations = 100000;
int adjacentToLeave = 4;
List<string> numbers;
void Main()
{
	var input = ReadMyFile(@"sample11.txt");
	if (live)
	{
		input = ReadMyFile(@"input11.txt");
	}
	numbers = ParseInput(input);

	char[,] map = new char[numbers.First().Length, numbers.Count()];
	for (int i = 0; i < numbers.First().Length; i++)
	{
		for (int j = 0; j < numbers.Count(); j++)
		{
			map[i, j] = numbers[j][i];
		}
	}
	//map.Dump();
	var simulatedMap = RunSimulation(map);
	CountOccupiedSeats(simulatedMap).Dump();
}

private int CountOccupiedSeats(char[,] map)
{
	int counter = 0;
	for (int k = 0; k < map.GetLength(0); k++)
		for (int l = 0; l < map.GetLength(1); l++)
		{
			if (map[k, l] == OCCUPIED)
				counter++;

		}

	return counter;
}

private char[,] RunSimulation(char[,] map)
{
	for (int i = 0; i < iterations; i++)
	{
		bool hasChanged = false;
		char[,] currentState = map.Clone() as char[,];
		for (int k = 0; k < map.GetLength(0); k++)
			for (int l = 0; l < map.GetLength(1); l++)
			{
				var change = TrySittingAtPosition(currentState, k, l);
				if (change != map[k, l])
				{
					hasChanged = true;
					map[k, l] = change;
				}

			}
		if (!hasChanged)
			return map;
	}

	return new char[1, 1];
}

private char TrySittingAtPosition(char[,] map, int x, int y)
{
	if (map[x, y] == EMPTY && IsSeatAlone(map, x, y))
	{
		return OCCUPIED;
	}
	else if (map[x, y] == OCCUPIED && ShouldVacate(map, x, y))
	{
		return EMPTY;
	}


	return map[x, y];
}

private bool ShouldVacate(char[,] map, int x, int y)
{
	if (OccupiedSeats(map, x, y) >= adjacentToLeave)
	{
		return true;
	}

	return false;
}

private bool IsSeatAlone(char[,] map, int x, int y)
{
	if (OccupiedSeats(map, x, y) > 0)
		return false;

	return true;
}

private int OccupiedSeats(char[,] map, int x, int y)
{
	int occupied = 0;
	if (x - 1 >= 0 && map[x - 1, y] == OCCUPIED)
		occupied++;
	if (x + 1 <= map.GetLength(0) - 1 && map[x + 1, y] == OCCUPIED)
		occupied++;
	if (y - 1 >= 0 && map[x, y - 1] == OCCUPIED)
		occupied++;
	if (y + 1 <= map.GetLength(1) - 1 && map[x, y + 1] == OCCUPIED)
		occupied++;
	if (x - 1 >= 0 && y - 1 >= 0 && map[x - 1, y - 1] == OCCUPIED)
		occupied++;
	if (x + 1 <= map.GetLength(0) - 1 && y + 1 <= map.GetLength(1) - 1 && map[x + 1, y + 1] == OCCUPIED)
		occupied++;
	if (y - 1 >= 0 && x + 1 <= map.GetLength(0) - 1 && map[x + 1, y - 1] == OCCUPIED)
		occupied++;
	if (x - 1 >= 0 && y + 1 <= map.GetLength(1) - 1 && map[x - 1, y + 1] == OCCUPIED)
		occupied++;

	return occupied;
}


private List<string> ParseInput(List<string> inputs)
{
	var list = new List<string>();

	foreach (var input in inputs)
	{

		list.Add(input);
	}

	return list;
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
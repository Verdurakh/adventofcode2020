<Query Kind="Program" />


bool live = true;
const int highestDiff = 3;
List<int> numbers;
Dictionary<int, long> cache;
void Main()
{
	cache = new Dictionary<int, long>();
	var input = ReadMyFile(@"sample10b.txt");
	if (live)
	{
		input = ReadMyFile(@"input10.txt");
	}
	numbers = ParseInput(input).OrderBy(w => w).ToList();

	ProcessNumber(0).Dump();
}



private long ProcessNumber(int number)
{
	long count = 0;
	if (cache.ContainsKey(number))
	{
		return cache[number];
	}

	var usableList = numbers.Where(w => w <= number + highestDiff && w > number && w != number).ToList();

	if (!usableList.Any())
	{
		return 1;
	}
	foreach (var num in usableList)
	{
		count += ProcessNumber(num);
	}
	if (!cache.ContainsKey(number))
	{
		cache.Add(number, count);
	}
	return count;
}


private List<int> ParseInput(List<string> inputs)
{
	var list = new List<int>();

	foreach (var input in inputs)
	{

		list.Add(int.Parse(input));
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
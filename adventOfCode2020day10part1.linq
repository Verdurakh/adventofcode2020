<Query Kind="Program" />


bool live = false;
const int highestDiff = 3;
Dictionary<int, int> joltUsage;
List<int> numbers;
void Main()
{
	joltUsage = new Dictionary<int, int>();
	var input = ReadMyFile(@"sample10b.txt");
	if (live)
	{
		input = ReadMyFile(@"input10.txt");
	}
	numbers = ParseInput(input).OrderBy(w => w).ToList();

	ProcessNumber(0);

	joltUsage.Dump();
	(joltUsage[1] * joltUsage[3]).Dump();

}

private void AddOrUpdateDic(int number)
{
	if (joltUsage.ContainsKey(number))
	{
		joltUsage[number]++;
	}
	else
	{
		joltUsage.Add(number, 1);
	}
}

private void ProcessNumber(int number)
{
	var usableList = numbers.Where(w => w <= number + highestDiff);
	var currentNumber = 0;
	if (!usableList.Any())
	{
		AddOrUpdateDic(highestDiff);
		currentNumber = number + highestDiff;
		return;
	}

	currentNumber = usableList.OrderBy(w => w).First();
	AddOrUpdateDic(currentNumber - number);
	numbers.Remove(currentNumber);

	ProcessNumber(currentNumber);
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
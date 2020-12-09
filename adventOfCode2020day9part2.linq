<Query Kind="Program" />

List<long> numbers;
int preamble = 5;
bool live = true;
void Main()
{
	var input = ReadMyFile(@"sample9.txt");
	if (live)
	{
		preamble = 25;
		input = ReadMyFile(@"input9.txt");
	}
	numbers = ParseInput(input);
	long numberMissing = 0;
	for (int i = preamble; i < numbers.Count() - 1; i++)
	{
		if (!CheckIndex(i))
		{
			numberMissing = numbers[i];

		}
	}
	($"{numberMissing} was the missing number").Dump();
	var indexes = FindSetOfNumbersThatMatch(numberMissing);
	($"Found index:{indexes.Item1} and {indexes.Item2}").Dump();
	var newList=numbers.Take(indexes.Item2+1).Skip(indexes.Item1);
	var highest=newList.OrderBy(w=>w).First();
	var lowest=newList.OrderByDescending(w=>w).First();

	($"Result:{lowest}+{highest}= {highest+lowest}").Dump();

}

private (int, int) FindSetOfNumbersThatMatch(long numberToFind)
{
	long sum = 0;
	($"Looking for indexes that sum {numberToFind}").Dump();
	for (int i = 0; i < numbers.Count(); i++)
	{
		sum = 0;
		for (int ii = i; ii < numbers.Count(); ii++)
		{
			sum += numbers[ii];
			if (sum == numberToFind)
			{
				return (i, ii);
			}

			if (sum > numberToFind)
			{
				break;
			}


		}
	}

	return (0, 0);
}

private bool CheckIndex(int index)
{
	var numberToCheck = numbers[index];
	var checkAgainst = numbers.Skip(index - preamble).Take(preamble).ToList();

	for (int i = 0; i < preamble; i++)
	{
		for (int ii = 0; ii < preamble; ii++)
		{
			if (checkAgainst[i] + checkAgainst[ii] == numberToCheck)
			{
				//($"{numberToCheck} was {checkAgainst[ii]}+{checkAgainst[i]} ").Dump();
				return true;
			}

		}
	}


	return false;
}



private List<long> ParseInput(List<string> inputs)
{
	var list = new List<long>();

	foreach (var input in inputs)
	{

		list.Add(long.Parse(input));
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
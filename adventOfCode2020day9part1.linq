<Query Kind="Program" />

List<long> numbers;
int preamble = 25;
void Main()
{

	var input = ReadMyFile(@"input9.txt");
	numbers = ParseInput(input);

	for (int i = preamble; i < numbers.Count() - 1; i++)
	{
		CheckIndex(i);
	}

}

private void CheckIndex(int index)
{
	var numberToCheck = numbers[index];
	var checkAgainst = numbers.Skip(index - preamble).Take(preamble).ToList();

	bool numberFound = false;
	for (int i = 0; i < preamble; i++)
	{
		for (int ii = 0; ii < preamble; ii++)
		{
			if (checkAgainst[i] + checkAgainst[ii] == numberToCheck)
			{
				//($"{numberToCheck} was {checkAgainst[ii]}+{checkAgainst[i]} ").Dump();
				numberFound = true;
			}

		}
	}

	if (!numberFound)
		($"{numberToCheck} was never found").Dump();
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
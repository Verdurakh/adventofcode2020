<Query Kind="Program" />

const string targetBag = "shinygold";
Dictionary<string, Bag> dic;
void Main()
{
	dic = new Dictionary<string, Bag>();
	var input = ReadMyFile(@"input7.txt");
	var list = ParseInput(input);
	FollowBag(targetBag).Dump();
}

private int FollowBag(string bag)
{
	var counter = 0;

	if (bag != targetBag && dic.ContainsKey(bag))
	{
		counter++;
	}

	if (dic.ContainsKey(bag) && dic[bag].Holds.Any())
	{
		foreach (var newBag in dic[bag].Holds)
		{
			for (int i = 0; i < newBag.Item2; i++)
			{
				counter += FollowBag(newBag.Item1);
			}
		}
	}

	return counter;
}

private List<Bag> ParseInput(List<string> inputs)
{
	var list = new List<Bag>();


	foreach (var input in inputs)
	{
		var bag = new Bag(input);

		if (!dic.ContainsKey(bag.Name))
		{
			dic.Add(bag.Name, bag);
		}

		list.Add(bag);
	}

	return list;
}

class Bag
{
	public string Name { get; set; }
	public List<(string, int)> Holds { get; set; }

	public Bag(string input)
	{
		Holds = new List<(string, int)>();
		var strs = input.Split(' ');
		Name = CreateName(strs[0] + strs[1]);
		int location = 7;
		if (strs[3].Equals("contain"))
		{
			while (location < strs.Count() && (strs[location].Contains(",") || strs[location].Contains(".")))
			{
				Holds.Add((CreateName(strs[location - 2] + strs[location - 1]), int.Parse(strs[location - 3])));
				location += 4;
				if (location > strs.Count() - 1)
					break;
			}
		}
	}

	private string CreateName(string name)
	{
		return name.Replace(",", "").Replace(".", "");
	}
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
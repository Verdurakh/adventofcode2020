<Query Kind="Program" />

const string targetBag = "shinygold";
Dictionary<string, Bag> dic;
void Main()
{
	dic = new Dictionary<string, Bag>();
	var input = ReadMyFile(@"input7.txt");
	var list = ParseInput(input);
	
	FindGoldenBags().Dump();
	
}

private int FindGoldenBags()
{
	var counter = 0;
	foreach (var element in dic.Where(w=>w.Key!=targetBag))
	{
		counter += FollowBag(element.Key);

	}

	return counter;
}

private int FollowBag(string bag)
{
	var counter = 0;
	//bag.Dump();

	if (bag.Equals(targetBag)){
		counter++;
		return counter;
	}
		

	if (dic.ContainsKey(bag) &&  dic[bag].Holds.Any())
	{
		foreach (var newBag in dic[bag].Holds)
		{
			//counter += FollowBag(newBag.Item1);
			if(FollowBag(newBag.Item1)>0)
			return 1;
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
		Name =CreateName(strs[0] + strs[1]);
		//strs.Dump();
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
	
	private string CreateName(string name){
		return name.Replace(",","").Replace(".","");
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




// Define other methods and classes here
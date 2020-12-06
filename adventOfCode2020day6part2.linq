<Query Kind="Program" />

void Main()
{
	var input = ReadMyFile(@"input6.txt");
	var groups = ParseInput(input);
	groups.Sum(w => w.YesAnswers).Dump();
	groups.ForEach(g => g.CalculateEveryoneAnsweredYes());
	groups.Sum(w=>w.EveryonePoints).Dump();
}



private List<Group> ParseInput(List<string> inputs)
{
	var list = new List<Group>();

	var group = new Group();
	foreach (var input in inputs)
	{
		if (string.IsNullOrEmpty(input))
		{
			list.Add(group);
			group = new Group();
		}

		group.FeedData(input);
	}

	list.Add(group);

	return list;
}


class Group
{
	public Dictionary<char, int> Answers;
	public int YesAnswers;
	public int NumberOfPeople;

	public int EveryonePoints;

	public Group()
	{
		Answers = new Dictionary<char, int>();
	}

	public void CalculateEveryoneAnsweredYes()
	{
	
		foreach (var dic in Answers)
		{
			if (dic.Value == NumberOfPeople)
			
				EveryonePoints++;
		}
	
	}

	public void FeedData(string input)
	{
		if (string.IsNullOrEmpty(input))
			return;

		NumberOfPeople++;
		foreach (var letter in input.ToCharArray())
		{

			if (!Answers.ContainsKey(letter))
			{
				Answers.Add(letter, 1);
				YesAnswers++;
			}
			else
			{
				var value = Answers[letter];
				Answers[letter] = value + 1;
			}
		}
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
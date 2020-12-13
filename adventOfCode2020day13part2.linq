<Query Kind="Program" />

bool live = true;
List<Bus> numbers;

void Main()
{
	var input = ReadMyFile(@"sample13c.txt").Dump();
	if (live)
	{
		input = ReadMyFile(@"input13.txt");
	}
	numbers = ParseInput(input);

	FindBus().Dump();


}

long FindBus()
{

	var time = 0L;
	var inc = numbers[0].Id;
	for (var i = 1; i < numbers.Count(); i++)
	{
		if (numbers[i].Id < 0)
			continue;

		var newTime = numbers[i].Id;
		while (true)
		{
			time += inc;
			if ((time + i) % newTime == 0)
			{
				inc *= newTime;
				break;
			}
		}

	}
	return time;
}


private List<Bus> ParseInput(List<string> inputs)
{
	var list = new List<Bus>();


	var busses = inputs[1].Split(',');

	foreach (var input in busses)
	{
		Bus bus;
		if (int.TryParse(input.ToString(), out int id))
		{
			bus = new Bus(id);

		}
		else
		{
			bus = new Bus();
		}
		list.Add(bus);

	}



	return list;
}

class Bus
{
	public long Id { get; set; }

	public Bus()
	{
		Id = -1;
	}

	public Bus(long id)
	{
		Id = id;
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
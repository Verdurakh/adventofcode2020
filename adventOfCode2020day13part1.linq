<Query Kind="Program" />

bool live = true;
List<Bus> numbers;

int currentTimestamp = 0;


void Main()
{
	var input = ReadMyFile(@"sample13.txt");
	if (live)
	{
		input = ReadMyFile(@"input13.txt");
	}
	numbers = ParseInput(input);

	var earliest = numbers.Where(w => w.EarliestTime > 0).OrderBy(w => w.EarliestTime).FirstOrDefault();
	earliest.Dump();
	var timeToWait = earliest.EarliestTime - currentTimestamp;
	timeToWait.Dump();

	var result = earliest.Id * timeToWait;
	result.Dump();


}


private List<Bus> ParseInput(List<string> inputs)
{
	var list = new List<Bus>();


	currentTimestamp = int.Parse(inputs[0]);
	var busses = inputs[1].Split(',');

	foreach (var input in busses)
	{

		if (int.TryParse(input.ToString(), out int id))
		{
			var bus = new Bus(id, currentTimestamp);
			list.Add(bus);
		}
	}



	return list;
}

class Bus
{
	int scheduleRange = 20;
	public int Id { get; set; }
	public int EarliestTime { get; set; }
	public List<int> Schedule { get; set; }

	public Bus(int id, int timestamp)
	{
		Id = id;

		Schedule = new List<int>();

		for (int i = Id; i < timestamp + scheduleRange; i += Id)
		{
			if (i >= timestamp)
				Schedule.Add(i);
		}

		EarliestTime = Schedule.FirstOrDefault();

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
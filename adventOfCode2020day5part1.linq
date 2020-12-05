<Query Kind="Program" />

const char Front = 'F'; //lower half
const char Back = 'B'; // upper half
const char Left = 'L'; // lower half
const char Right = 'R'; // upper half

void Main()
{
	var input = ReadMyFile(@"input5.txt");
	var tickets = CreateTickets(input);
	var highestTicketId=tickets.OrderByDescending(w=>w.SeatId);
	highestTicketId.First().Dump();
}

private List<Ticket> CreateTickets(List<string> inputs)
{
	var list = new List<Ticket>();


	foreach (var input in inputs)
	{
		var ticket = new Ticket(input);
		list.Add(ticket);
	}

	return list;
}


class Ticket
{
	public string Code { get; set; }
	private List<int> Rows { get; set; }
	private List<int> Columns { get; set; }
	public int Row { get; set; }
	public int Column { get; set; }
	public int SeatId { get; set; }

	public Ticket(string code)
	{
		Code = code;
		Rows = new List<int>();
		Columns = new List<int>();
		for (int i = 0; i < 128; i++)
		{
			Rows.Add(i);
		}
		for (int i = 0; i < 8; i++)
		{
			Columns.Add(i);
		}

		foreach (var letter in Code)
		{
			ParseCode(letter);
		}
	}

	public void ParseCode(char letter)
	{
		int breakPoint = 0;
		if (letter == Front || letter == Back)
			breakPoint = Rows[Rows.Count / 2];
		else
			breakPoint = Columns[Columns.Count / 2];

		switch (letter)
		{
			case Front:
				Rows = Rows.Where(w => w < breakPoint).ToList();
				break;
			case Back:
				Rows = Rows.Where(w => w >= breakPoint).ToList();
				break;
			case Left:
				Columns = Columns.Where(w => w < breakPoint).ToList();
				break;
			case Right:
				Columns = Columns.Where(w => w >= breakPoint).ToList();
				break;
		}

		if (Columns.Count == 1)
			Column = Columns[0];

		if (Rows.Count == 1)
			Row = Rows[0];
			
			SeatId=Row*8+Column;

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
<Query Kind="Program" />

const string Acc = "acc";
const string Jump = "jmp";
const string Nop = "nop";
Dictionary<int, Instruction> dic;
Dictionary<int, Instruction> visited;
int accumulator = 0;
void Main()
{
	visited = new Dictionary<int, Instruction>();
	dic = new Dictionary<int, Instruction>();
	var input = ReadMyFile(@"input8.txt");
	var list = ParseInput(input);

	FollowInstructions(0);
	accumulator.Dump();

}

private void FollowInstructions(int step)
{
	//step.Dump();
	if (visited.ContainsKey(step))
	{
		//"hey".Dump();
		return;
	}

	visited.Add(step, dic[step]);
	step += ParseInstruction(dic[step]);
 	FollowInstructions(step);
}

private int ParseInstruction(Instruction instruction)
{

	switch (instruction.Command)
	{
		case Acc:
			accumulator += instruction.Steps;
			return 1;
		case Jump:
			return instruction.Steps;
		case Nop:
			return 1;
	}


	return 0;
}


private List<Instruction> ParseInput(List<string> inputs)
{
	var list = new List<Instruction>();

	int pos = 0;
	foreach (var input in inputs)
	{
		var instruction = new Instruction(input, pos);

		if (!dic.ContainsKey(instruction.Position))
		{
			dic.Add(pos, instruction);
		}

		list.Add(instruction);
		pos++;
	}

	return list;
}

class Instruction
{
	public string Command;
	public int Steps;
	public int Position;

	public Instruction(string input, int position)
	{
		Position = position;

		var step = input.Split(' ');
		Command = step[0];
		Steps = int.Parse(step[1]);
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
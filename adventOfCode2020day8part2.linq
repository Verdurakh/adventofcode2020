<Query Kind="Program" />

const string Acc = "acc";
const string Jump = "jmp";
const string Nop = "nop";
Dictionary<int, Instruction> dic;
Dictionary<int, Instruction> visited;
int accumulator = 0;
int instructionTester = 0;
void Main()
{
	dic = new Dictionary<int, Instruction>();
	var input = ReadMyFile(@"input8.txt");
	var list = ParseInput(input);


	RunProgram(list);
	accumulator.Dump();
}

private void RunProgram(List<Instruction> backup)
{
	
	bool finished = false;
	do
	{
		visited = new Dictionary<int, Instruction>();
		accumulator = 0;
		finished = FollowInstructions(0);
		if (!finished)
			TryChangingInstruction(backup);
	} while (!finished);




}

private void TryChangingInstruction(List<Instruction> backup)
{
	dic = new Dictionary<int, UserQuery.Instruction>();
	int i = 0;
	foreach (var instruction in backup)
	{
		dic.Add(i, instruction.Copy());
		i++;
	}
	if (instructionTester > backup.Count() - 1)
		return;

	var instr = dic[instructionTester];
	if (instr.Command == Jump)
	{
		//($"Pos {instructionTester} changed {instr.Command} to {Nop}").Dump();
		instr.Command = Nop;

	}
	else if (instr.Command == Nop)
	{
		//($"Pos {instructionTester} changed {instr.Command} to {Jump}").Dump();
		instr.Command = Jump;
	}

	instructionTester++;
}

private bool FollowInstructions(int step)
{
	//step.Dump();
	if (visited.ContainsKey(step))
	{

		return false;
	}

	if (step > dic.Count() - 1)
	{

		return true;
	}

	visited.Add(step, dic[step]);
	step += ParseInstruction(dic[step]);
	return FollowInstructions(step);
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


		list.Add(instruction.Copy());
		pos++;
	}

	return list;
}

class Instruction
{
	public string Command;
	public int Steps;
	public int Position;

	public Instruction Copy()
	{
		var inst = new Instruction();
		inst.Command = Command;
		inst.Position = Position;
		inst.Steps = Steps;
		return inst;
	}

	public Instruction()
	{

	}

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
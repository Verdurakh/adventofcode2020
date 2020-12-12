<Query Kind="Program" />

bool live = true;
List<Instruction> numbers;
const char MOVE_FORWARD = 'F';
const char MOVE_NORTH = 'N';
const char MOVE_SOUTH = 'S';
const char MOVE_WEST = 'W';
const char MOVE_EAST = 'E';
const char TURN_RIGHT = 'R';
const char TURN_LEFT = 'L';

const double DIR_EAST =Math.PI/2;
const double DIR_SOUTH = 0;
const double DIR_WEST = Math.PI+Math.PI/2;
const double DIR_NORTH = Math.PI;

double direction = DIR_EAST;

double x = 0;
double y = 0;

void Main()
{
	var input = ReadMyFile(@"sample12.txt");
	if (live)
	{
		input = ReadMyFile(@"input12.txt");
	}
	numbers = ParseInput(input);

	foreach (var instr in numbers)
	{
		FollowInstructions(instr);
	}

	CalculateManhattanDistance(0, (int)x, 0, (int)y).Dump();
}

void FollowInstructions(Instruction instruction)
{

	switch (instruction.Code)
	{
		case MOVE_FORWARD:
			MoveInDirection(direction, instruction.Value);
			break;
		case MOVE_NORTH:
		case MOVE_EAST:
		case MOVE_SOUTH:
		case MOVE_WEST:
			MoveInDirection(ConvertMoveCodeToDirection(instruction.Code), instruction.Value);
			break;
		case TURN_LEFT:
			Turn(instruction.Value);
			break;
		case TURN_RIGHT:
			Turn(-instruction.Value);
			break;
	}
}

private void Turn(double degrees)
{
	//($"Turn {degrees} degrees").Dump();
	direction += ToRadians(degrees);
}


private double ConvertMoveCodeToDirection(char code)
{
	switch (code)
	{
		case MOVE_NORTH:
			return DIR_NORTH;
		case MOVE_EAST:
			return DIR_EAST;
		case MOVE_SOUTH:
			return DIR_SOUTH;
		case MOVE_WEST:
			return DIR_WEST;
	}

	throw new InvalidDataException("Not a valid code: " + code);
}

public static int CalculateManhattanDistance(int x1, int x2, int y1, int y2)
{
	return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}

private void MoveInDirection(double direction, int speed)
{
	//($"Move in direction {direction}. {speed} steps").Dump();
	this.x += speed * (int)Math.Sin(direction);
	this.y += speed * (int)Math.Cos(direction);
	//($"New value x:{x},y:{y}").Dump();
}

double ToRadians(double angleIn10thofaDegree)
{
	// Angle in 10th of a degree
	return (angleIn10thofaDegree * Math.PI) / 180;
}


class Instruction
{
	public char Code { get; set; }
	public int Value { get; set; }

	public Instruction(string input)
	{
		Code = input.Substring(0)[0];
		Value = int.Parse(input.Substring(1));
	}
}

private List<Instruction> ParseInput(List<string> inputs)
{
	var list = new List<Instruction>();

	foreach (var input in inputs)
	{
		var newInstruction = new Instruction(input);
		list.Add(newInstruction);
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
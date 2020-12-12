<Query Kind="Program" />

bool live = false;
List<Instruction> numbers;
const char MOVE_FORWARD = 'F';
const char MOVE_NORTH = 'N';
const char MOVE_SOUTH = 'S';
const char MOVE_WEST = 'W';
const char MOVE_EAST = 'E';
const char TURN_RIGHT = 'R';
const char TURN_LEFT = 'L';

const double DIR_EAST = Math.PI / 2; //1.5
const double DIR_SOUTH = 0;
const double DIR_WEST = Math.PI + Math.PI / 2; // 4,7
const double DIR_NORTH = Math.PI; // 3.14

double direction = DIR_EAST;

double x = 0;
double y = 0;

double wayX = 10;
double wayY = -1;
// 25568 too low
//27482 too low
//46529 too low
// 79664 too high
void Main()
{
	var input = ReadMyFile(@"sample12c.txt");
	if (live)
	{
		input = ReadMyFile(@"input12.txt");
	}
	numbers = ParseInput(input);

	foreach (var instr in numbers)
	{
		FollowInstructions(instr);
	}


	CalculateManhattanDistance((int)x, (int)y).Dump();
}

void FollowInstructions(Instruction instruction)
{
	($"Command {instruction.Code}, {instruction.Value}").Dump();
	switch (instruction.Code)
	{
		case MOVE_FORWARD:
			MoveToWaypoint(direction, instruction.Value);
			break;
		case MOVE_NORTH:
		case MOVE_EAST:
		case MOVE_SOUTH:
		case MOVE_WEST:
			MoveWaypointInDirection(ConvertMoveCodeToDirection(instruction.Code), instruction.Value, true);
			break;
		case TURN_LEFT:
			RotateWaypoint(-instruction.Value);
			break;
		case TURN_RIGHT:
			RotateWaypoint(instruction.Value);
			break;
	}
}

private void RotateWaypoint(double degrees)
{
	($"Rotate waypoint from x:{wayX} y:{wayY}").Dump();
	if (degrees > 0)
	{
		if (degrees == 90)
		{
			var temp = wayX;
			wayX = wayY * -1;
			wayY = temp;
		}

		if (degrees == 180)
		{
			wayX *= -1;
			wayY *= -1;
		}
	}
	else
	{
		if (degrees == -90)
		{
			var temp = wayX;
			wayX = wayY;
			wayY = temp * -1;
		}

		if (degrees == -180)
		{
			wayX *= -1;
			wayY *= -1;
		}
	}
	($"Rotated waypoint to x:{wayX} y:{wayY}").Dump();
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

public int CalculateManhattanDistance(int x, int y)
{
	return Math.Abs(x) + Math.Abs(y);
}
private void MoveWaypointInDirection(double direction, double speed, bool chat = false)
{
	if (chat)
		($"Move waypoint(x:{wayX}, y:{wayY}) {speed} steps").Dump();
	this.wayX += speed * (int)Math.Sin(direction);
	this.wayY += speed * (int)Math.Cos(direction);
	if (chat)
		($"New waypoint x:{wayX},y:{wayY}").Dump();
}
private void MoveToWaypoint(double direction, int times)
{
	($"Moving to waypoint({wayX},{wayY}) from x:{x},y:{y}. {times} times").Dump();
	for (int i = 0; i < times; i++)
	{
		this.x += wayX;
		this.y += wayY;
	}

	($"Moved to waypoint x:{x},y:{y}").Dump();
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
<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	inputList = GetSampleList();
	var res = FindPairWithValue(inputList, 2020);
	res.Dump();
	var answer = MultiplyValues(res.Item1, res.Item2,res.Item3).Dump();
	if (answer != 514579)
	{
		"Fail".Dump();
	}
	else
	{
		"Sample is working".Dump();
	}
	
	var res2=FindPairWithValue(ReadMyFile(@"d:\input.txt"),2020);
	res2.Dump();
	var answer2=MultiplyValues(res2.Item1,res2.Item2,res2.Item3);
	answer2.Dump();
}


private List<int> ReadMyFile(string uri){
var newList= new List<int>();
string line;
	System.IO.StreamReader reader = new StreamReader(uri);
	while ((line = reader.ReadLine()) != null)
	{
		if(int.TryParse(line,out int res)){
			newList.Add(res);
		}
		else{
			"was not a int".Dump();
		}
	}
	
	return newList;
}


private int MultiplyValues(int x, int y,int z)
{
	return x * y*z;
}

private (int, int,int) FindPairWithValue(List<int> inputs, int value)
{

	foreach (var val in inputs)
	{
		foreach (var testValue in inputs)
		{
			foreach (var testValue2 in inputs)
			{
				if (val + testValue +testValue2 == value)
					return (val, testValue,testValue2);
			}
			
		}
	}


	return (0, 0,0);
}

private List<int> GetSampleList()
{
	var newList = new List<int>();
	newList.Add(1721);
	newList.Add(979);
	newList.Add(366);
	newList.Add(299);
	newList.Add(675);
	newList.Add(1456);
	return newList;
}

// Define other methods and classes here

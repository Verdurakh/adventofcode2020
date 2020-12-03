<Query Kind="Program" />

void Main()
{
	var inputList = new List<string>();
	inputList = GetSampleList().Dump();
	var passwords=ParseStringsToPassword(inputList).Dump();
	int counter=0;
	foreach (var pw in passwords)
	{
		if(pw.IsValid())
		counter++;
	}
	counter.Dump();
	//var res = FindPairWithValue(inputList, 2020);
	//res.Dump();
	//var answer = MultiplyValues(res.Item1, res.Item2,res.Item3).Dump();
	//if (answer != 514579)
	//{
	//	"Fail".Dump();
	//}
	//else
	//{
	//	"Sample is working".Dump();
	//}
	//
	inputList=ReadMyFile(@"d:\input2.txt");
	passwords = ParseStringsToPassword(inputList).Dump();
	counter = 0;
	foreach (var pw in passwords)
	{
		if (pw.IsValid())
			counter++;
	}
	counter.Dump();
	//res2.Dump();
	//var answer2=MultiplyValues(res2.Item1,res2.Item2,res2.Item3);
	//answer2.Dump();
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

private List<Password> ParseStringsToPassword(List<string> strs){
var list=new List<Password>();
foreach (var str in strs)
{
	string substring=str;
		var pw = new Password();
		int location=substring.IndexOf("-");
		var minString = substring.Substring(0, location);
		substring=substring.Substring(location+1);
		pw.minOccurence = int.Parse(minString);
		location=substring.IndexOf(" ");
		var maxOc=substring.Substring(0, location);
		location=substring.IndexOf(" ");
		pw.MaxOccurence=int.Parse(maxOc);
		substring=substring.Substring(substring.IndexOf(" "));
		pw.Character=substring.Substring(0,substring.IndexOf(":")).Replace(" ","");
		substring=substring.Substring(str.IndexOf(" ")+1);
		
		pw.PwString=str.Substring(str.IndexOf(":")+1).Replace(" ","");
		
		list.Add(pw);
	}

	
	
	return list;
}



private List<string> GetSampleList()
{
	var newList = new List<string>();
	newList.Add("1-3 a: abcde");
	newList.Add("1-3 b: cdefg");
	newList.Add("2-9 c: ccccccccc");

	return newList;
}


class Password
{
	public int minOccurence { get; set; }
	public int MaxOccurence { get; set; }
	public string Character { get; set; }

	public string PwString { get; set; }
	
	public bool IsValid(){
		var count=PwString.Count(w=>w==Character.ToCharArray()[0]);
		return count>=minOccurence && count<=MaxOccurence;
	}
}

// Define other methods and classes here

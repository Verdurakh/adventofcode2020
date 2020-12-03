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

	inputList=ReadMyFile(@"d:\input2.txt");
	passwords = ParseStringsToPassword(inputList).Dump();
	counter = 0;
	foreach (var pw in passwords)
	{
		if (pw.IsValid())
			counter++;
	}
	counter.Dump();

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
		bool hit=false;
		if(PwString[minOccurence-1].ToString().Equals(Character))
		hit=true;
		
		if(PwString[MaxOccurence-1].ToString().Equals(Character)){
			if(hit)
			return false;
			
			hit=true;
		}
		
		
		return hit;
	}
}

// Define other methods and classes here

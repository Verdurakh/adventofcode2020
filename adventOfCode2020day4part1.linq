<Query Kind="Program" />





void Main()
{


	var test = ReadMyFile(@"sample4.txt");
	//test.Dump();
	var ports=ParsePassports(test);
	ports.Dump();
	
	foreach (var passport in ports)
	{
		passport.IsValid().Dump();
	}

}

private List<Passport> ParsePassports(List<string> lines)
{
	var list = new List<Passport>();
	var pass = new Passport();
	foreach (var line in lines)
	{
		if (string.IsNullOrEmpty(line))
		{
			list.Add(pass);
			pass = new Passport();
		}

		pass.FeedData(line);

	}
	
	list.Add(pass);




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

class Passport
{
	const string byr = "byr";
	const string iyr = "iyr";
	const string eyr = "eyr";
	const string hgt = "hgt";
	const string hcl = "hcl";
	const string ecl = "ecl";
	const string pid = "pid";
	const string cid = "cid";
	List<(string, bool)> reqFields;
	public string Byr { get; set; }
	public string Iyr { get; set; }
	public string Eyr { get; set; }
	public string Hgt { get; set; }
	public string Hcl { get; set; }
	public string Ecl { get; set; }
	public string Pid { get; set; }
	public string Cid { get; set; }

	public Passport()
	{
		reqFields = new List<(string, bool)>() { (byr, true), (iyr, true), (eyr, true), (hgt, true), (hcl, true), (ecl, true), (pid, true), (cid, false) };
	}

	public void FeedData(string line)
	{
		foreach (var value in reqFields)
		{
			if (line.Contains(value.Item1))
			{
				var str = line.Substring(line.IndexOf(value.Item1));
				if (str.IndexOf(" ") > 0)
					str = str.Substring(0, str.IndexOf(" "));
				str = str.Replace(value.Item1, "").Substring(1);
				SetData(str,value.Item1);
			}
		}
	}
	
	public bool IsValid(){
		foreach (var elem in reqFields.Where(w=>w.Item2))
		{
			if(!CheckData(elem.Item1))
			return false;
		}
		
		return true;
	}

	private bool CheckData(string field)
	{
		switch (field)
		{
			case byr:
				return !string.IsNullOrEmpty(Byr);
			case iyr:
				return !string.IsNullOrEmpty(Iyr);
			case eyr:
				return !string.IsNullOrEmpty(Eyr);
			case hgt:
				return !string.IsNullOrEmpty(Hgt);
			case hcl:
				return !string.IsNullOrEmpty(Hcl);
			case ecl:
				return !string.IsNullOrEmpty(Ecl);
			case pid:
				return !string.IsNullOrEmpty(Pid);
			case cid:
				return !string.IsNullOrEmpty(Cid);

		}
		
		return false;
	}

	private void SetData(string value, string field)
	{
		switch (field)
		{
			case byr:
				Byr = value;
				break;
			case iyr:
				Iyr = value;
				break;
			case eyr:
				Eyr = value;
				break;
			case hgt:
				Hgt = value;
				break;
			case hcl:
				Hcl = value;
				break;
			case ecl:
				Ecl = value;
				break;
			case pid:
				Pid = value;
				break;
			case cid:
				Cid = value;
				break;

		}
	}
}



// Define other methods and classes here
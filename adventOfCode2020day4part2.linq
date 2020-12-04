<Query Kind="Program" />





void Main()
{


	var test = ReadMyFile(@"input4.txt");
	//test.Dump();
	var ports = ParsePassports(test);
	ports.Dump();
	var count = 0;
	foreach (var passport in ports)
	{
		if (passport.IsValid())
			count++;
	}

	count.Dump();
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
	public int? Byr { get; set; }
	public int? Iyr { get; set; }
	public int? Eyr { get; set; }
	public string Hgt { get; set; }
	public string Hcl { get; set; }
	public string Ecl { get; set; }
	public int? Pid { get; set; }
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
				SetData(str, value.Item1);
			}
		}
	}

	public bool IsValid()
	{
		foreach (var elem in reqFields.Where(w => w.Item2))
		{
			if (!CheckData(elem.Item1))
				return false;
		}

		return true;
	}

	private bool CheckData(string field)
	{
		switch (field)
		{
			case byr:
				return Byr != null;
			case iyr:
				return Iyr != null;
			case eyr:
				return Eyr != null;
			case hgt:
				return !string.IsNullOrEmpty(Hgt);
			case hcl:
				return !string.IsNullOrEmpty(Hcl);
			case ecl:
				return !string.IsNullOrEmpty(Ecl);
			case pid:
				return Pid != null;
			case cid:
				return !string.IsNullOrEmpty(Cid);

		}

		return false;
	}

	private int? DigitWithRange(string value, int digits, int min, int max)
	{
		if (int.TryParse(value, out int res))
		{
			if (value.Length != digits)
				return null;

			if (res > max || res < min)
				return null;

			return res;
		}
		return null;
	}

	private string CheckHeight(string value)
	{
		if (value.Contains("cm") && DigitWithRange(value.Replace("cm", ""), 3, 150, 193) != null)
			return value;
		if (value.Contains("in") && DigitWithRange(value.Replace("in", ""), 2, 59, 76) != null)
			return value;

		return null;
	}
	
	private string CheckHairColor(string value){
		if(!value[0].Equals('#'))
		return null;
		if(value.Substring(1).Length!=6)
		return null;

		Regex r = new Regex("[^a-f0-9]$");
		if (r.IsMatch(value.Substring(1)))
		{
			// validation failed
			return null;
		}

	return value;
	
	}

	private string CheckEyeColor(string value)
	{
		var eyes = new List<string>() {"amb","blu","brn","gry","grn","hzl","oth"};
		
		foreach (var eye in eyes)
		{
			if(eye==value)
			return value;
		}
		
		
		return null;
	}

	private void SetData(string value, string field)
	{
		switch (field)
		{
			case byr:
				Byr = DigitWithRange(value, 4, 1920, 2002);
				break;
			case iyr:
				Iyr = DigitWithRange(value, 4, 2010, 2020);
				break;
			case eyr:
				Eyr = DigitWithRange(value, 4, 2020, 2030);
				break;
			case hgt:
				Hgt = CheckHeight(value);
				break;
			case hcl:
				Hcl = CheckHairColor(value);
				break;
			case ecl:
				Ecl = CheckEyeColor(value);
				break;
			case pid:
				Pid = DigitWithRange(value, 9, 0, int.MaxValue);
				break;
			case cid:
				Cid = value;
				break;

		}
	}
}



// Define other methods and classes here
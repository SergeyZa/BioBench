bool IsValid(int x)
{
	var s = 0;
	for (var a = 0; s <= x; a++)
	{
		var s1 = a * 3;
		s = s1;
		for (var b = 0; s < x; b++)
		{
			var s2 = b * 7;
			s = s1 + s2;
			for (var c = 0; s <= x; c++)
			{
				var s3 = c * 11;
				s = s1 + s2 + s3;
				if (s == x)
					return true;
			}
			s = s1 + s2;
		}
		s = s1;
	}
	return false;
}

bool IsValidScore(int x, int y)
{
	return IsValid(x) && IsValid(y);
}

var scores = new List<Tuple<int, int>>
{
	new Tuple<int,int>(10,11),
			new Tuple<int,int>(8,7),
			new Tuple<int,int>(10, 13),
			new Tuple<int,int>(1,192),
			new Tuple<int,int>(7,5),
			new Tuple<int,int>(2,53),
			new Tuple<int,int>(5,600),
			new Tuple<int,int>(122,601),
			new Tuple<int,int>(35,8)
		};

foreach (var score in scores)
	Console.WriteLine($"{score} => {IsValidScore(score.Item1, score.Item2)}");

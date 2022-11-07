using level19;

string input = File.ReadAllText("sample.txt");
var bla = input.Split("\r\n\r\n").Select(t => t.Split(Environment.NewLine));

var scanners = bla.Select(t => new Scanner(t)).ToList();

foreach (var scanner in scanners)
{
    scanner.CalculateDistances();
    scanner.PrintDistances();
}

List<Point> points = new List<Point>();

foreach (var s1 in scanners)
{
    foreach (var s2 in scanners)
    {
        if (s1 == s2) continue;

        var set = s1.CompareDistances(s2);

        foreach (var d in set)
        {
            if (!points.Contains(d.first.Start))
                points.Add(d.first.Start);

            if (!points.Contains(d.first.End))
                points.Add(d.first.End);
        }
    }
}

Console.WriteLine(points.Count);
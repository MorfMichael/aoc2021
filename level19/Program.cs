using level19;

string input = File.ReadAllText("sample.txt");
var bla = input.Split("\r\n\r\n").Select(t => t.Split(Environment.NewLine));

var scanners = bla.Select(t => new Scanner(t)).ToList();

foreach (var scanner in scanners)
{
    scanner.CalculateDistances();
    scanner.PrintDistances();
}

List<Point[]> points = new List<Point[]>();

foreach (var scanner in scanners)
{
    foreach (var d in scanner.Distances)
    {
        var other = scanners.SelectMany(t => t.Distances).Where(t => t.Value == d.Value).ToList();
        if (other.Any())
        {
            var starts = new[] { d.Start }.Concat(other.Select(t => t.Start)).ToArray();
            if (starts.All(x => !points.SelectMany(t => t).Contains(x)))
                points.Add(starts);

            var ends = new[] { d.End }.Concat(other.Select(t => t.End)).ToArray();
            if (ends.All(x => !points.SelectMany(t => t).Contains(x)))
                points.Add(ends);
        }
        else
        {
            if (!points.Any(t => t.Contains(d.Start)))
                points.Add(new[] { d.Start });
            if (!points.Any(t => t.Contains(d.End)))
                points.Add(new[] { d.End });
        }
    }
}

Console.WriteLine("the end!");
using level19;

string input = File.ReadAllText("sample.txt");
var bla = input.Split("\r\n\r\n").Select(t => t.Split(Environment.NewLine));

var scanners = bla.Select(t => new Scanner(t)).ToList();

foreach (var scanner in scanners)
{
    scanner.CalculateDistances();
    scanner.PrintDistances();
}

var same = scanners[0].Distances.Where(t => scanners.Skip(1).Any(x => x.Distances.Any(d => d.distance == t.distance))).ToList();
Console.WriteLine(same);
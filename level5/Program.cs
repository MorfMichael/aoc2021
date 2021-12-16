string[] input = File.ReadAllLines("input.txt");

var data = input.Select(t =>
{
    var row = t.Split(" -> ").Select(x =>
    {
        var a = x.Split(",").Select(y => int.Parse(y)).ToList();
        return (X: a[0], Y: a[1]);
    }).ToList();

    return new { Start = row[0], End = row[1] };
}).ToList();

List<(int X, int Y)> result = new List<(int X, int Y)>();

foreach (var entry in data)
{
    if (entry.Start.X != entry.End.X || entry.Start.Y != entry.End.Y)
        continue;

    if (entry.Start.X == entry.End.X)
    {
        
    }
    else if (entry.Start.Y == entry.End.Y)
    {

    }
}
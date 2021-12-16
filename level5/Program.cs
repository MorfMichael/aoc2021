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
    if (!(entry.Start.X == entry.End.X || entry.Start.Y == entry.End.Y))
        continue;

    if (entry.Start.X == entry.End.X)
    {
        int min = Min(entry.Start.Y, entry.End.Y);
        int max = Max(entry.Start.Y, entry.End.Y);
        for (int i = min; i <= max; i++)
        {
            result.Add((entry.Start.X, i));
        }
    }
   
    if (entry.Start.Y == entry.End.Y)
    {
        int min = Min(entry.Start.X, entry.End.X);
        int max = Max(entry.Start.X, entry.End.X);
        for (int i = min; i <= max; i++)
        {
            result.Add((i, entry.Start.Y));
        }
    }
}

int output = result.GroupBy(x => x).Where(t => t.Count() > 1).Count();
File.WriteAllText("output.txt", output.ToString());
Console.WriteLine(output);


int Min(int a, int b) => a < b ? a : b;
int Max(int a, int b) => a > b ? a : b;
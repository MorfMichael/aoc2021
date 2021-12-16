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
    if (entry.Start.X == entry.End.X)
    {
        var (min, max) = MinMax(entry.Start.Y, entry.End.Y);
        for (int i = min; i <= max; i++)
        {
            result.Add((entry.Start.X, i));
        }
    }
    else if (entry.Start.Y == entry.End.Y)
    {
        var (min, max) = MinMax(entry.Start.X, entry.End.X);
        for (int i = min; i <= max; i++)
        {
            result.Add((i, entry.Start.Y));
        }
    }
    else if (Math.Abs(entry.Start.X - entry.End.X) == Math.Abs(entry.Start.Y - entry.End.Y))
    {
        int count = Math.Abs(entry.Start.X - entry.End.X);
        for (int i = 0; i <= count; i++)
        {
            int x = entry.Start.X + (entry.Start.X < entry.End.X ? i : -i);
            int y = entry.Start.Y + (entry.Start.Y < entry.End.Y ? i : -i);
            result.Add((x, y));
        }
    }
}

int output = result.GroupBy(x => x).Where(t => t.Count() > 1).Count();
File.WriteAllText("output.txt", output.ToString());
Console.WriteLine(output);


(int Min, int Max) MinMax(int a, int b) => a < b ? (Min: a, Max: b) : (Min: b, Max: a);

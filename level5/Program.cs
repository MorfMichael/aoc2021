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

//int width = data.Max(x => x.Start.X > x.End.X ? x.Start.X : x.End.X);
//int height = data.Max(y => y.Start.Y > y.End.Y ? y.Start.Y : y.End.Y);

List<(int X, int Y)> result = new List<(int X, int Y)>();

foreach (var entry in data)
{

}


int x = 1;
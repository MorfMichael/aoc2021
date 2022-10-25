string[] lines = File.ReadAllLines("input.txt");
//string[] lines = File.ReadAllLines("example.txt");

List<Point> map = lines.SelectMany((line, y) => line.Select((cell, x) => new Point(x, y, int.Parse(cell.ToString()), line.Length, lines.Length))).ToList();

List<int> basins = new List<int>();
foreach (var point in map)
{
    var neighbours = point.GetNeighbours(map).ToList();

    if (neighbours.All(x => x.Value > point.Value))
    {
        List<Point> basin = new List<Point> { point };
        Recursion(point, map, basin);
        basins.Add(basin.Count);
    }
}

int sum = basins.OrderByDescending(x => x).Take(3).Aggregate(1, (a, b) => a * b);
Console.WriteLine(sum);

void Recursion(Point point, List<Point> map, List<Point> basins)
{
    var neighbours = point.GetNeighbours(map);

    foreach (var neighbour in neighbours)
    {
        if (neighbour.Value > point.Value && neighbour.Value != 9 && !basins.Contains(neighbour))
        {
            basins.Add(neighbour);
            Recursion(neighbour, map, basins);
        }
    }
}

class Point
{
    private int _width;
    private int _height;

    public Point(int x, int y, int value, int width, int height)
    {
        _width = width;
        _height = height;

        X = x;
        Y = y;
        Value = value;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Value { get; set; }

    public List<(int X, int Y)> GetNeighbours()
    {
        List<(int X, int Y)> neighbours = new List<(int X, int Y)> { (X - 1, Y), (X + 1, Y), (X, Y - 1), (X, Y + 1) };
        return neighbours.Where(t => t.X >= 0 && t.X < _width && t.Y >= 0 && t.Y < _height).ToList();
    }

    public List<Point> GetNeighbours(List<Point> map)
    {
        List<(int X, int Y)> neighbours = new List<(int X, int Y)> { (X - 1, Y), (X + 1, Y), (X, Y - 1), (X, Y + 1) };
        return neighbours.Where(t => t.X >= 0 && t.X < _width && t.Y >= 0 && t.Y < _height).Select(t => map.FirstOrDefault(x => x.X == t.X && x.Y == t.Y)).ToList();
    }
}
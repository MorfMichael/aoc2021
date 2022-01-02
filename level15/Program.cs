//string[] lines = File.ReadAllLines("example.txt");
string[] lines = File.ReadAllLines("input.txt");

List<Point> map = lines.SelectMany((t, y) => t.Select((d, x) => new Point(x, y, int.Parse(d.ToString())))).ToList();

Point current = map.First();
Point end = map.Last();

List<Point> watched = new List<Point>();
List<(Point Point, int Cost)> check = new List<(Point, int)> { (current, 0) };

(Point Point, int Cost) entry = check.First();

while (check.Any())
{
    entry = check.OrderBy(t => t.Cost).First();
    check.Remove(entry);

    if (watched.Contains(entry.Point))
        continue;

    if (entry.Point == end)
    {
        break;
    }

    watched.Add(entry.Point);

    var neighbours = map.Where(x => entry.Point.IsNeighbour(x)).ToList();

    foreach (var neighbour in neighbours)
    {
        check.Add((neighbour, entry.Cost + neighbour.Value));
    }
}

Console.WriteLine(entry.Point);
Console.WriteLine(entry.Cost);

void PrintMap(List<Point> map, List<Point> path)
{
    List<List<Point>> grouped = map.GroupBy(t => t.Y).Select(t => t.ToList()).ToList();

    foreach (var row in grouped)
    {
        foreach (var point in row)
        {
            if (path.Contains(point))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write(point.Value);
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
    }
}

class Point
{
    public Point(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Value { get; set; }

    public int Distance(Point point) => Math.Abs(point.Y - Y) + Math.Abs(point.X - X);

    public bool IsNeighbour(Point point) => (point.X == X && (point.Y == Y - 1 || point.Y == Y + 1)) || (point.Y == Y && (point.X == X - 1 || point.X == X + 1));

    public override string ToString() => $"{X};{Y};{Value}";
}
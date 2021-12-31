string[] lines = File.ReadAllLines("example.txt");

Stack<Point> path = new Stack<Point>();
List<Point> visited = new List<Point>();

List<Point> map = lines.SelectMany((t,y) => t.Select((d,x) => new Point(x,y,int.Parse(d.ToString())))).ToList();
PrintMap();

int count = 0;
Point start = map.First();
Point bla = map.Skip(11).First();
Point end = map.Last();
List<Point> watched = new List<Point>();

int c = start.Count(map, end, watched);

Point current = map.Last();

Console.WriteLine(count);

void PrintMap()
{
    Console.WriteLine(string.Join(Environment.NewLine, map.GroupBy(t => t.Y).Select(row => string.Join(string.Empty, row.Select(t => t.Value)))));
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

    public int Distance(Point point) => Math.Abs(point.X - X) + Math.Abs(point.Y - Y);

    public int Count(List<Point> map, Point point, List<Point> watched)
    {
        watched.Add(this);

        if (point == this)
            return 1;

        var ncount = map.Where(t => !watched.Contains(t) && IsNeighbour(t)).Select(t => t.Count(map, point, watched)).OrderBy(t => t).First();

        return 1 + ncount;
    }

    public override string ToString() => $"{X},{Y},{Value}";

    public bool IsNeighbour(Point point) => (point.X == X && (point.Y == Y-1 || point.Y == Y+1)) || (point.Y == Y && (point.X == X - 1 || point.X == X + 1));
}
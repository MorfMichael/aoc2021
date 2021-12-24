string[] lines = File.ReadAllLines("input.txt");
//string[] lines = File.ReadAllLines("example.txt");

List<Point> map = lines.SelectMany((line,y) => line.Select((cell,x) => new Point(x,y,int.Parse(cell.ToString()), line.Length, lines.Length))).ToList();

List<int> lowest = new List<int>();

foreach (var point in map)
{
    var neighbours = point.GetNeighbours().Select(t => map.FirstOrDefault(x => x.X == t.X && x.Y == t.Y)).ToList();

    if (neighbours.All(x => x.Value > point.Value))
    {
        lowest.Add(point.Value);
    }
}

int sum = map.Where(t => t.GetNeighbours().Select(x => map.FirstOrDefault(m => m.X == x.X && m.Y == x.Y)?.Value).All(x => t.Value < x.Value)).Sum(x => 1 + x.Value);

Console.WriteLine(sum);


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
        List<(int X, int Y)> neighbours = new List<(int X, int Y)> { (X-1, Y), (X+1, Y), (X, Y-1), (X, Y+1) };
        return neighbours.Where(t => t.X >= 0 && t.X < _width && t.Y >= 0 && t.Y < _height).ToList();
    }
}
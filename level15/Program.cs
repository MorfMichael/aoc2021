//string[] lines = File.ReadAllLines("example.txt");
string[] lines = File.ReadAllLines("input.txt");

int[][] map = lines.Select(t => t.Select(d => int.Parse(d.ToString())).ToArray()).ToArray();

List<(int X, int Y, int Cost)> costs = new List<(int X, int Y, int Cost)> { (0, 0, 0) };
List<(int X, int Y)> visited = new List<(int X, int Y)>();

(int X, int Y, int Cost) cur = costs.First();

while (costs.Any())
{
    cur = costs.OrderBy(t => t.Cost).First();
    costs.Remove(cur);

    if (visited.Contains((cur.X, cur.Y))) continue;
    visited.Add((cur.X, cur.Y));

    if (cur.Y == map.Length - 1 && cur.X == map[0].Length - 1) break;

    foreach ((int x, int y) in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
    {
        int X = cur.X + x;
        int Y = cur.Y + y;

        if (!(0 <= X && X < map[0].Length && 0 <= Y && Y < map.Length)) continue;

        costs.Add((X, Y, cur.Cost + map[Y][X]));
    }
}

Console.WriteLine($"{cur.X},{cur.Y}");
Console.WriteLine(cur.Cost);
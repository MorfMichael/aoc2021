//string[] lines = File.ReadAllLines("example.txt");
string[] lines = File.ReadAllLines("input.txt");

int[][] map = lines.Select(t => t.Select(d => int.Parse(d.ToString())).ToArray()).ToArray();

int height = map.Length;
int width = map[0].Length;

int[][] new_map = new int[map.Length*5][];

for (int y = 0; y < height * 5; y++)
{
    new_map[y] = new int[width * 5];
    for (int x = 0; x < width * 5; x++)
    {
        new_map[y][x] = GetValue(x, y, map);
    }
}

//PrintMap(new_map);

List<(int X, int Y, int Cost)> costs = new List<(int X, int Y, int Cost)> { (0, 0, 0) };
List<(int X, int Y)> visited = new List<(int X, int Y)>();

var cur = costs.First();

while (costs.Any())
{
    cur = costs.OrderBy(t => t.Cost).First();
    costs.Remove(cur);

    if (visited.Contains((cur.X, cur.Y))) continue;
    visited.Add((cur.X, cur.Y));

    if (cur.Y == new_map.Length - 1 && cur.X == new_map[0].Length - 1) break;

    foreach ((int x, int y) in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
    {
        int X = cur.X + x;
        int Y = cur.Y + y;

        if (!(0 <= X && X < new_map[0].Length && 0 <= Y && Y < new_map.Length)) continue;

        costs.Add((X, Y, cur.Cost + new_map[Y][X]));
    }
}

Console.WriteLine($"{cur.X},{cur.Y}");
Console.WriteLine(cur.Cost);

void PrintMap(int[][] map)
{
    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[0].Length; x++)
        {
            Console.Write(map[y][x]);
        }
        Console.WriteLine();
    }
}


int GetValue(int x, int y, int[][] map)
{
    int xx = x % width;
    int yy = y % height;

    int quadrant_x = x / width;
    int quadrant_y = y / height;

    int value = map[yy][xx] + quadrant_x + quadrant_y;
    if (value > 9)
        value = value - 9;

    return value;
}
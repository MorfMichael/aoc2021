string[] lines = File.ReadAllLines("example.txt");

int[][] map = lines.Select(t => t.Select(x => int.Parse(x.ToString())).ToArray()).ToArray();


int count = 0;
int x = 0, y = 0;
int width = map[0].Length, height = map.Length;

while (x < map[0].Length - 1 || y < map.Length - 1)
{
    Console.WriteLine(map[y][x]);
    int right = Sum(x + 1, y);
    int down = Sum(x, y + 1);

    if (right <= down)
    {
        Console.WriteLine("down");
        x++;
    }
    else
    {
        Console.WriteLine("right");
        y++;
    }

    count += map[y][x];
}

int Sum(int x, int y)
{
    int count = map[y][x];

    if (x < map[0].Length - 1)
        count += map[y][x + 1];
    if (y < map.Length - 1)
        count += map[y + 1][x];

    return count;
}

Console.WriteLine($"{x}, {y}, {count}");
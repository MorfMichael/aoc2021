//string[] lines = File.ReadAllLines("example.txt");
//string[] lines = File.ReadAllLines("example1.txt");
string[] lines = File.ReadAllLines("input.txt");

int[][] map = lines.Select(t => t.Select(x => int.Parse(x.ToString())).ToArray()).ToArray();
int flashes = 0;
//int steps = 100;
bool all = false;
int i = 0;

PrintMap();

while (!all)
{
    i++;

    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            map[y][x]++;
        }
    }

    var toflash = map.SelectMany((t, y) => t.Select((a, x) => (x: x, y: y, value: a)).Where(a => a.value > 9).Select(a => (a.x, a.y))).ToList();
    var flashed = new List<(int x, int y)>();
    foreach (var flash in toflash)
    {
        Flash(flash.x, flash.y, flashed);
    }

    if (flashed.Count == map.Length * map[0].Length)
    {
        all = true;
    }

    foreach (var flash in flashed)
    {
        map[flash.y][flash.x] = 0;
    }

    PrintMap();
}

Console.WriteLine(i);

List<(int x, int y)> Adjacent(int x, int y)
{
    var adjacents = new List<(int x, int y)>
    {
        (x-1, y-1), (x,y-1), (x+1,y-1),
        (x-1,y), (x+1,y),
        (x-1,y+1), (x,y+1), (x+1,y+1)
    };

    return adjacents.Where(t => t.x >= 0 && t.y >= 0 && t.y < map.Length && t.x < map[t.y].Length).ToList();
}

void Flash(int x, int y, List<(int, int)> flashed)
{
    if (flashed.Contains((x, y)))
        return;

    flashed.Add((x, y));
    flashes++;

    var adjacents = Adjacent(x, y);
    foreach (var a in adjacents)
    {
        map[a.y][a.x]++;
        if (map[a.y][a.x] > 9)
        {
            Flash(a.x, a.y, flashed);
        }
    }
}

Console.WriteLine(flashes);

void PrintMap()
{
    for (int i = 0; i < map.Length; i++)
    {
        for (int j = 0; j < map[i].Length; j++)
        {
            Console.Write(map[i][j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
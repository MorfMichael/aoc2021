string[] lines = File.ReadAllLines("level20.in");
string source = lines[0];
lines = lines[2..];
int iterations = 2;

char[,] map = new char[lines.Length,lines[0].Length];

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        map[y, x] = lines[y][x];
    }
}

for (int i = 0; i < iterations; i++)
{
    bool on = i % 2 == 1;
    map = Wrap(map, 2, on);
    map = Enhancement(map, on);
}

int count = 0;
for (int y = 0; y < map.GetLength(0); y++)
{
    for (int x = 0; x < map.GetLength(1); x++)
    {
        if (map[y, x] == '#') count++;
    }
}

Console.WriteLine(count);

char[,] Enhancement(char[,] input, bool on)
{
    char[,] output = new char[input.GetLength(0), input.GetLength(1)];
    for (int y = 0; y < input.GetLength(0); y++)
    {
        for (int x = 0; x < input.GetLength(1); x++)
        {
            char pixel = GetPixel(x, y, input, on);
            output[y, x] = pixel;
        }
    }

    return output;
}

char GetPixel(int x, int y, char[,] input, bool on)
{
    List<(int x, int y)> nbs = new()
    {
        (x-1,y-1), (x,y-1), (x+1,y-1),
        (x-1,y), (x,y), (x+1,y),
        (x-1,y+1), (x,y+1), (x+1,y+1),
    };

    string number = "";
    foreach (var n in nbs)
    {
        if (n.x >= 0 && n.x < input.GetLength(1) && n.y >= 0 && n.y < input.GetLength(0))
            number += input[n.y,n.x] == '#' ? "1": "0";
        else
            number += on ? "1" : "0";
    }

    int idx = Convert.ToInt32(number, 2);
    return source[idx];
}

void Print(char[,] map)
{
    for (int y = 0; y < map.GetLength(0); y++)
    {
        for (int x = 0; x < map.GetLength(1); x++)
        {
            Console.Write(map[y, x]);
        }
        Console.WriteLine();
    }
}

char[,] Wrap(char[,] input, int count, bool on)
{
    char[,] output = new char[input.GetLength(0) + count*2, input.GetLength(1) + count*2];

    for (int y = 0; y < output.GetLength(0); y++)
    {
        for (int x = 0; x < output.GetLength(1); x++)
        {
            if (x < count || x > (input.GetLength(1)-1+count) || y < count || y > (input.GetLength(0)-1+count)) output[y, x] = on ? '#' : '.';
            else output[y, x] = input[y - count, x - count];
        }
    }

    return output;
}
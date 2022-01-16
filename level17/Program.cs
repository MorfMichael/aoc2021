//string input = "target area: x=20..30, y=-10..-5";
string input = File.ReadAllText("input.txt");

input = input.Replace("target area: ", "").Replace("x=", "").Replace("y=", "");
int[] width = input.Split(", ")[0].Split("..").Select(t => int.Parse(t)).ToArray();
int[] height = input.Split(", ")[1].Split("..").Select(t => int.Parse(t)).ToArray();


Dictionary<(int x, int y), List<(int x, int y)>> result = new Dictionary<(int x, int y), List<(int x, int y)>>();

int xv = 1, yv = 1;
int x = 0, y = 0;
int Y = 0;

for (int a = 0; a < 500; a++)
{
    for (int b = -1000; b < 1000; b++)
    {
        xv = a;
        yv = b;
        x = y = 0;
        bool hit = false;
        List<(int x, int y)> points = new List<(int, int)>();

        for (int i = 0; i < 1000; i++)
        {
            x += xv;
            y += yv;

            points.Add((x, y));

            if (x >= width[0] && x <= width[1] && y >= height[0] && y <= height[1])
            {
                hit = true;
                break;
            }

            xv += xv == 0 ? 0 : (xv > 0 ? -1 : 1);
            yv--;
        }

        if (hit)
        {
            result.Add((a, b), points);
        }
    }
}

var blaa = result.OrderByDescending(t => t.Value.Max(x => x.y)).First().Value.Max(x => x.y);
Console.WriteLine(result.Count);

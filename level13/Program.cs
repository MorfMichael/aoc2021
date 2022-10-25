//string file = "example.txt";
string file = "input.txt";

string[] lines = File.ReadAllLines(file);

List<(int x, int y)> dots = lines.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(t =>
{
    var split = t.Split(",");
    return (int.Parse(split[0]), int.Parse(split[1]));
}).ToList();

List<(string direction, int position)> instructions = lines.Reverse().TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(t =>
{
    var split = t.Replace("fold along ", "").Split("=");
    return (split[0], int.Parse(split[1]));
}).Reverse().ToList();

int width = dots.Max(x => x.x);
int height = dots.Max(x => x.y);

foreach (var inst in instructions)
{
    Console.WriteLine($"{inst.direction}, {inst.position}, {width}, {height}");

    List<(int x, int y)> a_dots = new List<(int x, int y)>();
    List<(int x, int y)> b_dots = new List<(int x, int y)>();

    if (inst.direction == "x")
    {
        a_dots = dots.Where(t => t.x < inst.position).ToList();
        b_dots = dots.Where(t => t.x > inst.position).Select(t => (width - t.x, t.y)).ToList();
        width = width/2-1;
    }
    else
    {
        a_dots = dots.Where(t => t.y < inst.position).ToList();
        b_dots = dots.Where(t => t.y > inst.position).Select(t => (t.x, height - t.y)).ToList();
        height = height/2-1;
    }

    dots = a_dots.Union(b_dots).Distinct().ToList();

    PrintDots();
}

//HECRZKPR

void PrintDots()
{
    if (height < 10)
    {
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                Console.Write(dots.Contains((x, y)) ? "x" : " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("dots: " + dots.Count + Environment.NewLine);
    }
}
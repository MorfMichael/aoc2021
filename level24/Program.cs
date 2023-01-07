using System.Runtime.CompilerServices;
using System.Transactions;
using System.Xml.Schema;

/*
/inp w\nmul x 0\nadd x z\nmod x 26\ndiv z (1|26)\nadd x (-?\d+)\neql x w\neql x 0\nmul y 0\nadd y 25\nmul y x\nadd y 1\nmul z y\nmul y 0\nadd y w\nadd y (-?\d+)\nmul y x\nadd z y/gm
 */

string[] lines = File.ReadAllLines("level24.in");

int[] model = new int[14] { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

List<(int a, int b, int c)> inst = new();
Dictionary<(int w, int i, int z), int> cache = new();

var ops = lines.Chunk(18).ToList();

foreach (var op in ops)
{
    int a = int.Parse(op[4].Split()[2]);
    int b = int.Parse(op[5].Split()[2]);
    int c = int.Parse(op[15].Split()[2]);
    Console.WriteLine((a, b, c));
    inst.Add((a, b, c));
}

while (true)
{
    int z = 0;
    if (Operate(model, z))
    {
        Console.WriteLine(Print(model));
        return;
    }
    else
    {
        model = Subtract(model);
    }
}


bool Operate(int[] model, int z)
{
    int w = 0, x = 0, y = 0;

    for (int i = 0; i < 14; i++)
    {
        w = model[i];

        var key = (w, i, z);
        if (cache.ContainsKey(key)) z = cache[key];
        else
        {
            (int a, int b, int c) = inst[i];
            x *= 0;
            x += z;
            x %= 26;
            z /= a;
            x += b;
            x = x == w ? 1 : 0;
            x = x == 0 ? 1 : 0;
            y *= 0;
            y += 25;
            y *= x;
            y++;

            int y1 = y;
            z *= y;
            y *= 0;
            y += w;
            y += c;
            y *= x;
            z += y;
            Console.WriteLine((w,x,y,z));
        }
    }

    return z == 0;
}

string Print(int[] model) => string.Join("", model);

int[] Subtract(int[] model)
{
    model[13]--;

    int i = 13;
    while (i >= 0 && model.Any(t => t <= 0))
    {
        if (model[i] <= 0)
        {
            model[i] = 9;

            if (i > 0)
                model[i - 1]--;
        }
        i--;
    }

    return model;
}

int[] Add(int[] model)
{
    model[13]++;

    int i = 13;
    while (i >= 0 && model.Any(t => t > 9))
    {
        if (model[i] > 9)
        {
            model[i] = 1;

            if (i > 0)
                model[i - 1]++;
        }
        i--;
    }

    return model;
}

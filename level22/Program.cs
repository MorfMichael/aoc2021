using System.Diagnostics;
using System.Numerics;
using System.Runtime.Serialization;
using System.Xml;

string[] lines = File.ReadAllLines("level22.in");

List<int> X = new();
List<int> Y = new();
List<int> Z = new();
List<Step> steps = new();
HashSet<(int x, int y, int z)> on = new();

for (int i = 0; i < lines.Length; i++)
{
    string line = lines[i];
    var split = line.Split();
    bool state = split[0] == "on";
    var coords = split[1].Split(',');

    int[] xs = coords[0][2..].Split("..").Select(int.Parse).ToArray();
    int[] ys = coords[1][2..].Split("..").Select(int.Parse).ToArray();
    int[] zs = coords[2][2..].Split("..").Select(int.Parse).ToArray();

    xs[1]++;
    ys[1]++;
    zs[1]++;

    X.Add(xs[0]);
    X.Add(xs[1]);
    Y.Add(ys[0]);
    Y.Add(ys[1]);
    Z.Add(zs[0]);
    Z.Add(zs[1]);
    
    steps.Add(new Step(xs[0], xs[1], ys[0], ys[1], zs[0], zs[1], state));
}

X = X.OrderBy(t => t).ToList();
Y = Y.OrderBy(t => t).ToList();
Z = Z.OrderBy(t => t).ToList();
int n = X.Count;

bool[,,] grid = new bool[n,n,n];

foreach (var step in steps)
{
    int x0 = X.IndexOf(step.x0);
    int x1 = X.IndexOf(step.x1);
    int y0 = Y.IndexOf(step.y0);
    int y1 = Y.IndexOf(step.y1);
    int z0 = Z.IndexOf(step.z0);
    int z1 = Z.IndexOf(step.z1);

    for (int x = x0; x < x1; x++)
    {
        for (int y = y0; y < y1; y++)
        {
            for (int z = z0; z < z1; z++)
            {
                if (step.state)
                    on.Add((x, y, z));
                else
                    on.Remove((x, y, z));
            }
        }
    }
}

long sum = 0;

foreach (var o in on)
{
    long x = X[o.x + 1] - X[o.x];
    long y = Y[o.y + 1] - Y[o.y];
    long z = Z[o.z + 1] - Z[o.z];
    long s = x * y * z;
    //Console.WriteLine($"{x} * {y} * {z} = {s}");
    sum += s;
}

//for (int x = 0; x < n-1; x++)
//{
//    for (int y = 0; y < n-1; y++)
//    {
//        for (int z = 0; z < n-1; z++)
//        {
//            if (grid[x,y,z])
//                sum += (X[x+1] - X[x]) * (Y[y+1] - Y[y]) * (Z[z+1] - Z[z]);
//        }
//    }
//}

Console.WriteLine(sum);


record Step(int x0,int x1, int y0, int y1, int z0, int z1, bool state);
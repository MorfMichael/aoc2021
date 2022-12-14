using System.Diagnostics;

string[] lines = File.ReadAllLines("sample2.in");

HashSet<(int x, int y, int z)> cubes = new();

foreach (var line in lines)
{
    var split = line.Split();
    string state = split[0];
    var coords = split[1].Split(',');

    int[] xs = coords[0][2..].Split("..").Select(int.Parse).ToArray();
    int[] ys = coords[1][2..].Split("..").Select(int.Parse).ToArray();
    int[] zs = coords[2][2..].Split("..").Select(int.Parse).ToArray();

    long xr = xs[1] - xs[0];
    long yr = ys[1] - ys[0];
    long zr = zs[1] - zs[0];

    Console.WriteLine($"x: {xr} * y: {yr} * z: {zr} = {xr*yr*zr}");

    HashSet<(int x, int y, int z)> check = new();
    for (int x = xs[0]; x <= xs[1]; x++)
    {
        //if (x < -50 || x > 50) continue;
        for (int y = ys[0]; y <= ys[1]; y++)
        {
            //if (y < -50 || y > 50) continue;
            for (int z = zs[0]; z <= zs[1]; z++)
            {
                //if (z < -50 || z > 50) continue;
                check.Add((x, y, z));
            }
        }
    }

    if (state == "on") foreach (var c in check) cubes.Add(c);
    if (state == "off") foreach (var c in check) cubes.Remove(c);
}

Console.WriteLine(cubes.Count);
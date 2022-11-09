using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using level19;
using Microsoft.VisualBasic;

string input = File.ReadAllText("sample.txt");
var lines = input.Split("\r\n\r\n").Select(t => t.Split(Environment.NewLine));

var scanners = lines.Select(t => new Scanner2(t)).ToList();

var beacons = scanners.SelectMany(t => t.Beacons).ToList();

foreach (var s1 in scanners)
{
    foreach (var b1 in s1.Beacons)
    {
        foreach (var s2 in scanners)
        {
            if (s1 == s2) continue;

            int c1 = 0;
            Beacon b = null;

            foreach (var b2 in s2.Beacons)
            {
                var tc = b1.Distances.Count(x => b2.Distances.Any(t => t.Item1 == x.Item1));
                if (tc > c1)
                {
                    c1 = tc;
                    b = b2;
                }
            }

            if (b != null) b1.Other.Add(b);
        }
    }
}

List<Beacon> check = new List<Beacon>();

int c = 0;


foreach (var beacon in scanners.SelectMany(t => t.Beacons))
{
    if (check.Contains(beacon)) continue;
    c++;
    check.Add(beacon);
    foreach (var o in beacon.Other) check.Add(o);
}

Console.WriteLine(c);
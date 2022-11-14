using System.Data;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Serialization;
using level19;
using Microsoft.VisualBasic;

string input = File.ReadAllText("sample.txt");
var lines = input.Split("\r\n\r\n").Select(t => t.Split(Environment.NewLine));

var scanners = lines.Select(t => new Scanner2(t)).ToList();

var beacons = scanners.SelectMany(t => t.Beacons).ToList();

var angles = new[] { (float)(Math.PI/180)*0, (float)(Math.PI / 180) * 90, (float)(Math.PI / 180) * 180, (float)(Math.PI / 180) * 270 };

var rotations = angles.SelectMany(x => angles.SelectMany(y => angles.Select(z => Quaternion.CreateFromYawPitchRoll(y,x,z)))).ToList();

var s1 = scanners[0];
foreach (var scanner in scanners.Skip(1))
{
    Dictionary<Vector3, float> bla = new Dictionary<Vector3, float>();
    foreach (var rotation in rotations)
    {
        var points = scanner.Rotate(rotation).ToList();
        var test = points.Select(t => s1.Beacons.Select(b => (t - b.Position).Round())).ToList();

        var found = test.SelectMany(t => t.Select(x => x)).GroupBy(x => x).FirstOrDefault(t => t.Count() >= 12);
        if (found != null)
        {
            var distance = found.Key;
            scanner.Normalize(rotation, distance);
            break;
        }
    }
}

var dots = scanners.SelectMany(t => t.Beacons.Select(x => x.Position)).Distinct().ToList();

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
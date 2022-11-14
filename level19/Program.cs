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

var angles = new[] { 0, (float)(Math.PI / 180) * 90, (float)(Math.PI / 180) * 180, (float)(Math.PI / 180) * 270 };

List<Quaternion> rotations = new List<Quaternion>();

foreach (var yaw in angles)
{
    foreach (var pitch in angles)
    {
        foreach (var roll in angles)
        {
            var quaternion = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll).Round();
            if (!rotations.Contains(quaternion)) rotations.Add(quaternion);
        }
    }
}

List<(Scanner2 a, Scanner2 b, Vector3 distance, Quaternion rotation)> result = new();

foreach (var s1 in scanners)
{
    foreach (var s2 in scanners)
    {
        if (s1 == s2) continue;

        Dictionary<Vector3, float> bla = new Dictionary<Vector3, float>();
        foreach (var rotation in rotations)
        {
            var points = s2.Rotate(rotation).ToList();
            var test = points.Select(t => s1.Beacons.Select(b => (t - b.Position).Round())).ToList();

            var found = test.SelectMany(t => t.Select(x => x)).GroupBy(x => x.Length()).FirstOrDefault(t => t.Count() >= 12);
            if (found != null)
            {
                result.Add((s1, s2, found.First(), rotation));
            }
        }
    }
}

var dots = scanners.SelectMany(t => t.Beacons.Select(x => x.Position)).Distinct().ToList();
Console.WriteLine(dots.Count);
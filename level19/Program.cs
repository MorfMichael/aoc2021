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

var axis = new List<Vector3>
{
    new Vector3(1,0,0),
    new Vector3(-1,0,0),
    new Vector3(0,1,0),
    new Vector3(0,-1,0),
    new Vector3(0,0,1),
    new Vector3(0,0,-1),
};

foreach (var a in axis)
{
    foreach (var angle in angles)
    {
        rotations.Add(Quaternion.CreateFromAxisAngle(a, angle).Round());
    }
}

List<(Scanner2 From, Scanner2 To, Quaternion Rotation, Vector3 Transform)> operations = new();
foreach (var s1 in scanners)
{
    foreach (var s2 in scanners)
    {
        if (s2.Id == 0 || s1 == s2) continue;

        if (operations.Any(t => (t.From == s1 && t.To == s2) || (t.From == s2 && t.To == s1))) continue;

        foreach (var rotation in rotations.Distinct())
        {
            var rotated = s2.Rotate(rotation).ToList();
            var distances = rotated.Select(t => new { t.Old, t.New, Distances = s1.Beacons.Select(b => new { End = b.Position, Distance = (b.Position - t.New).Round() }) }).ToList();

            var distance_length = distances.SelectMany(t => t.Distances.Select(x => x.Distance.Length())).Distinct().ToList();

            foreach (var length in distance_length)
            {
                var overlapping = distances.Where(t => t.Distances.Any(x => x.Distance.Length() == length)).ToList();
                if (overlapping.Count >= 12)
                {
                    Console.WriteLine($"{s2.Id} to {s1.Id}");
                    overlapping.ForEach(x => Console.WriteLine($"{x.Old}, {x.New}, {x.Distances.FirstOrDefault(t => t.Distance.Length() == length)?.End}"));
                }
            }


            //var found = rotated.SelectMany(t => t.Select(x => x)).GroupBy(x => x.Length()).FirstOrDefault(t => t.Count() >= 12);
            //if (found != null)
            //{
            //    operations.Add((s2, s1, rotation, found.First()));
            //    Console.WriteLine($"{s2.Id} to {s1.Id}: rotation {rotation}, distance {found.First()}");
            //    break;
            //}
        }
    }
}

//foreach (var scanner in scanners)
//{
//    var op = operations.FirstOrDefault(x => x.From == scanner);
//    while (op != default)
//    {
//        Console.WriteLine($"normalize {scanner.Id} adds from {op.From.Id} to { op.To.Id}");
//        scanner.Normalize(op.Rotation, op.Transform);
//        op = operations.FirstOrDefault(x => x.From == op.To);
//    }
//}

//var dots = scanners.SelectMany(t => t.Beacons.Select(x => x.Position)).Distinct().ToList();
//Console.WriteLine(dots.Count);
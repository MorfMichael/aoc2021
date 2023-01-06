using System.Linq.Expressions;
using System.Numerics;

// wrong answers: 499, 478

List<string[]> lines = File.ReadAllText("input.txt").Split("\r\n\r\n").Select(t => t.Split("\r\n")).ToList();
List<Scanner> scanners = lines.Select(Scanner.Parse).ToList();

float pi = (float)Math.PI;

float[] rad = { 0, pi / 2, pi, 3 * pi / 2 };
var rotations = rad.SelectMany(yaw => rad.SelectMany(pitch => rad.Select(roll => RoundQuaternion(yaw, pitch, roll)))).Distinct().ToList();

HashSet<(int s1, int s2)> scon = new();
Dictionary<(int s1, int s2), (Vector3 b1, Vector3 b2)[]> overlapping = new();

foreach (var scanner in scanners)
{
    var checks = scanner.Beacons.SelectMany(t => Check(scanner, t.Value).Select(x => (b1: t.Key, s2: x.scanner.Id, b2: x.beacon))).GroupBy(t => t.s2, t => (t.b1, t.b2)).ToList();
    foreach (var ch in checks) // overlapping scanner
    {
        if (!scon.Contains((ch.Key, scanner.Id)))
            scon.Add((scanner.Id, ch.Key));

        if (!overlapping.ContainsKey((ch.Key, scanner.Id)))
            overlapping.Add((scanner.Id, ch.Key), ch.ToArray());
    }
}

var sids = scanners.Select(t => t.Id).OrderByDescending(x => x).ToArray();

List<int[]> ways = new();
foreach (var id in sids)
{
    if (!scon.Any(t => t.s1 == id || t.s2 == id))
    {
        Console.WriteLine("no connection");
        continue;
    }

    var way = Find(id, scon);
    ways.Add(way);

    Console.WriteLine(string.Join(",", way));
}

List<HashSet<Vector3>> points = new();

foreach (var scanner in scanners)
{
    foreach (var beacon in scanner.Beacons)
    {
        if (!points.Any(t => t.Contains(beacon.Key)))
            points.Add(new() { beacon.Key });
    }
}

foreach (var o in overlapping)
{
    foreach (var pair in o.Value)
    {
        var ex1 = points.Where(t => t.Contains(pair.b1)).Select(t => (Element: pair.b2, List: t)).ToList();
        var ex2 = points.Where(t => t.Contains(pair.b2)).Select(t => (Element: pair.b1, List: t)).ToList();

        if (ex1.Count == 1 && ex2.Count == 0)
            ex1[0].List.Add(ex1[0].Element);
        else if (ex1.Count == 0 && ex2.Count == 1)
            ex2[0].List.Add(ex2[0].Element);
        else
        {
            ex1.ForEach(l => points.Remove(l.List));
            ex2.ForEach(l => points.Remove(l.List));

            var merged = ex1.SelectMany(t => t.List).Concat(ex2.SelectMany(t => t.List)).ToHashSet();
            merged.Add(pair.b1);
            merged.Add(pair.b2);
            points.Add(merged);
        }
    }
}

Console.WriteLine(points.Count);

int[] Find(int id, HashSet<(int s1, int s2)> options)
{
    List<(int id, int[] way)> Q = new() { (id, new[] { id }) };
    while (Q.Any())
    {
        var c = Q.OrderBy(x => x.way.Length).FirstOrDefault();
        Q.Remove(c);

        if (c.id == 0) return c.way;

        var op1 = options.Where(t => t.s1 == c.id).ToList();
        foreach (var o1 in op1)
        {
            if (c.way.Contains(o1.s2)) continue;

            var nway = c.way.Append(o1.s2).ToArray();
            Q.Add((o1.s2, nway));
        }

        var op2 = options.Where(t => t.s2 == c.id).ToList();
        foreach (var o2 in op2)
        {
            if (c.way.Contains(o2.s1)) continue;

            var nway = c.way.Append(o2.s1).ToArray();
            Q.Add((o2.s1, nway));
        }
    }

    return new[] { id };
}

IEnumerable<(Scanner scanner, Vector3 beacon)> Check(Scanner s, float[] distances)
{
    foreach (var scanner in scanners)
    {
        if (scanner == s) continue;
        foreach (var beacon in scanner.Beacons)
        {
            if (beacon.Value.Count(x => distances.Contains(x)) >= 11)
                yield return (scanner, beacon.Key);
        }
    }
}

float Round(double value, int precision = 0) => (float)Math.Round(value, precision);

Quaternion RoundQuaternion(float yaw, float pitch, float roll)
{
    Quaternion q = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
    return new Quaternion(Round(q.X, 4), Round(q.Y, 4), Round(q.Z, 4), Round(q.W, 4));
}

class Scanner
{
    public static Scanner Parse(string[] lines) => new Scanner(lines);

    public Scanner(string[] lines)
    {
        Id = int.Parse(lines[0].Replace("---", "").Replace(" scanner ", ""));
        var l = lines[1..];
        var beacons = l.Select(t => t.Split(',').Select(int.Parse).ToArray()).Select(t => new Vector3(t[0], t[1], t[2])).ToList();

        foreach (var beacon in beacons)
        {
            var distances = beacons.Where(x => x != beacon).Select(t => Vector3.Distance(beacon, t)).OrderBy(t => t).ToArray();
            Beacons.Add(beacon, distances);
        }
    }

    public int Id { get; set; }

    public Dictionary<Vector3, float[]> Beacons { get; set; } = new();

    public Vector3 Position { get; set; }

    public HashSet<Vector3> Rotated { get; set; }

    float Round(double value, int precision = 0) => (float)Math.Round(value, precision);

    Vector3 Rotate(Vector3 vector, Quaternion rotation)
    {
        var v = Vector3.Transform(vector, rotation);
        return new Vector3(Round(v.X), Round(v.Y), Round(v.Z));
    }

    public void Rotate(Quaternion q)
    {
        var rotated = Beacons.Select(t => Rotate(t.Key,q)).ToList();
    }
}
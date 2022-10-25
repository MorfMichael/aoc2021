string[] lines = File.ReadAllLines("example.txt");
//string[] lines = File.ReadAllLines("input.txt");

string template = lines.First();
Dictionary<char, ulong> result = template.GroupBy(t => t).ToDictionary(t => t.Key, t => (ulong)t.Count());

var pairs = Enumerable.Range(0, template.Length - 1).Select(t => new string(template.Skip(t).Take(2).ToArray())).GroupBy(t => t).ToDictionary(x => x.Key, x => (ulong)x.Count());
var rules = lines.Skip(2).ToDictionary(t => t[0..2], t => new List<string> { new string(new[] { t[0], t[6] }), new string(new[] { t[6], t[1] }) });


for (int step = 0; step < 40; step++)
{
    Dictionary<string, ulong> new_pairs = new Dictionary<string, ulong>();

    foreach (var pair in pairs)
    {
        var rule = rules[pair.Key];

        if (result.ContainsKey(rule[0][1]))
            result[rule[0][1]] += pair.Value;
        else
            result[rule[0][1]] = pair.Value;

        if (new_pairs.ContainsKey(rule[0]))
            new_pairs[rule[0]] += pair.Value;
        else
            new_pairs[rule[0]] = pair.Value;

        if (new_pairs.ContainsKey(rule[1]))
            new_pairs[rule[1]] += pair.Value;
        else
            new_pairs[rule[1]] = pair.Value;
    }

    pairs = new_pairs;
}

ulong min = result.Min(x => x.Value), max = result.Max(x => x.Value);
Console.WriteLine($"{max} - {min} = {max - min}");
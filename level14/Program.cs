//string[] lines = File.ReadAllLines("example.txt");
string[] lines = File.ReadAllLines("input.txt");

string template = lines.First();

var rules = lines.Skip(2).ToDictionary(t => t[0..2], t => new string(new[] { t[0], t[6], t[1] }));

List<string> result = new List<string>();

for (int step = 0; step < 10; step++)
{
    var pairs = Enumerable.Range(0, template.Length - 1).Select(t => new string(template.Skip(t).Take(2).ToArray())).ToList();
    template = template[0] + string.Join(string.Empty, pairs.Select(t => rules[t][1..]));
    result.Add(template);
}

int max = result.Max(t => t.GroupBy(x => x).Select(x => x.Count()).Max());
int min = result.Max(t => t.GroupBy(x => x).Select(x => x.Count()).Min());

Console.WriteLine($"{max} - {min} = {max - min}");
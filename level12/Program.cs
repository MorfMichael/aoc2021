using System.Collections.Concurrent;

string file = "example.txt";

string[] s_e = new[] { "start", "end" };
string[] lines = File.ReadAllLines(file);
ConcurrentDictionary<string, List<string>> caves = new ConcurrentDictionary<string, List<string>>();

foreach (var line in lines)
{
    var split = line.Split("-");
    var left = split[0];
    var right = split[1];

    if (!s_e.Contains(right))
    {
        caves.AddOrUpdate(left, new List<string> { right }, (key, list) =>
        {
            if (!list.Contains(right))
                list.Add(right);
            return list.OrderBy(t => t).ToList();
        });
    }

    if (!s_e.Contains(left))
    {
        caves.AddOrUpdate(right, new List<string> { left }, (key, list) =>
        {
            if (!list.Contains(left))
                list.Add(left);
            return list.OrderBy(t => t).ToList();
        });
    }
}

foreach (var cave in caves.Where(x => !s_e.Contains(x.Key)).OrderBy(t => t.Key))
{
    Console.WriteLine($"{cave.Key} [{string.Join(",", cave.Value)}]");
}

while ()
{
    string? cur = caves["start"].FirstOrDefault();
    List<string> ignore = new List<string>();
    List<string> path = new List<string>();

    while (cur != null)
    {
        Console.WriteLine(cur + ": " + string.Join(",", caves[cur].Where(t => !ignore.Contains(t))));

        path.Add(cur);

        if (cur != null && cur.All(char.IsLower))
            ignore.Add(cur);

        cur = caves[cur].FirstOrDefault(x => !ignore.Contains(x));
    }

    Console.WriteLine(string.Join(",", path));

void PrintCaves()
{
    Console.WriteLine(" --- CAVES --- ");
    foreach (var cave in caves)
    {
        Console.WriteLine($"{cave.Key} [{string.Join(",", cave.Value)}]");
    }
}
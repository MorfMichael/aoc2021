using System.Collections.Concurrent;

//string file = "example.txt";
string file = "input.txt";
string[] lines = File.ReadAllLines(file);
Dictionary<string, List<string>> caves = new Dictionary<string, List<string>>();

foreach (var line in lines)
{
    var split = line.Split("-");
    var left = split[0];
    var right = split[1];

    if (caves.ContainsKey(left)) caves[left].Add(right);
    else caves[left] = new List<string> { right };

    if (caves.ContainsKey(right)) caves[right].Add(left);
    else caves[right] = new List<string> { left };
}

Console.WriteLine(string.Join(Environment.NewLine, caves.Select(cave => $"{cave.Key} [{string.Join(",", cave.Value)}]")));

Queue<(string, List<string>)> q = new Queue<(string, List<string>)>();
q.Enqueue(("start", new List<string> { "start" }));

int count = 0;

while (q.Any())
{
    var (pos, small) = q.Dequeue();
    Console.WriteLine($"{pos}, {string.Join(",", small)}");

    if (pos == "end")
    {
        count++;
        continue;
    }

    foreach (var entry in caves[pos])
    {
        if (!small.Contains(entry))
        {
            var new_small = small.Distinct().ToList();
            if (entry.ToLower() == entry)
            {
                new_small.Add(entry);
            }

            q.Enqueue((entry, new_small));
        }
    }
}

Console.WriteLine(count);

void PrintCaves()
{
    Console.WriteLine(" --- CAVES --- ");
    foreach (var cave in caves)
    {
        Console.WriteLine($"{cave.Key} [{string.Join(",", cave.Value)}]");
    }
}
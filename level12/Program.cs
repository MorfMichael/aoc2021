using System.Collections.Concurrent;

string[] lines = File.ReadAllLines("example.txt");

ConcurrentDictionary<string, List<string>> caves = new ConcurrentDictionary<string, List<string>>();

foreach (var line in lines)
{
    var parts = line.Split("-");
    string left = parts[0], right = parts[1];
    caves.AddOrUpdate(left, new List<string> { right }, (key, list) => { if (!list.Contains(right)) list.Add(right); return list; });
    caves.AddOrUpdate(right, new List<string> { left }, (key, list) => { if (!list.Contains(left)) list.Add(left); return list; });
}

PrintCaves();

string curr = "start";
string prev = curr;
List<string> path = new List<string>();

while (curr != "end")
{
    if (curr.All(char.IsLower))
    {
        foreach (var cave in caves)
        {
            if (cave.Value.Contains(curr))
            {
                cave.Value.Remove(curr);
            }
        }
    }

    path.Add(curr);
    prev = curr;
    curr = caves[curr].FirstOrDefault();

    if (curr == "end")
    {
        path.Add(curr);
    }

    //PrintCaves();
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
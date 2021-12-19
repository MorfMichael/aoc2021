using System.Collections.Concurrent;
using System.Linq;

string input = File.ReadAllText("input.txt");
List<int> fishies = input.Split(",").Select(t => int.Parse(t)).ToList();
//List<int> fishies = new List<int> { 3, 4, 3, 1, 2 };

int days = 256;

var dict = fishies.GroupBy(t => t).ToDictionary(x => x.Key, x => (ulong)x.Count());

for (int day = 1; day <= days; day++)
{
    Console.WriteLine(day);

    Dictionary<int, ulong> current = new Dictionary<int, ulong>();

    foreach (var (key,value) in dict)
    {
        if (key == 0)
        {
            if (!current.ContainsKey(6))
                current.Add(6, value);
            else
                current[6] += value;

            if (!current.ContainsKey(8))
                current.Add(8, value);
            else
                current[8] += value;
        }
        else
        {
            if (!current.ContainsKey(key - 1))
                current.Add(key - 1, value);
            else
                current[key - 1] += value;
        }
    }

    dict = current;
}

ulong sum = dict.Aggregate<KeyValuePair<int,ulong>,ulong>(0, (a, b) => a += b.Value);
Console.WriteLine($"There will be {sum} fishies after {days} days!");

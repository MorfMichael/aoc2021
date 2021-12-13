using System.Linq;

int count = 0;
int? prev = null;
List<int> values = File.ReadAllLines("input.txt").Select(t => int.Parse(t)).ToList();
List<string> output = new List<string>();

foreach (var value in values) {
    if (prev == null)
    {
        prev = value;
    }
    else
    {
        if (value > prev) count++;
    }

    prev = value;
}

Console.WriteLine(count);
using System.IO;
using System.Linq;

int? prev = null;
int count = 0;
List<int> input = File.ReadAllLines("input.txt").Select(t => int.Parse(t)).ToList();

for (int i = 0; i < input.Count; i++) 
{
    int sum = input.Skip(i).Take(3).Sum();
    if (prev.HasValue && sum > prev) count++;
    prev = sum;
}

File.WriteAllText("output.txt", count.ToString());
Console.WriteLine(count);
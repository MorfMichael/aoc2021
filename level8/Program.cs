string[] input = File.ReadAllLines("input.txt");

List<(string[] Input, string[] Output)> signals = input.Select(t =>
{
    var split = t.Split("|");
    return (Input: split[0].Split(" ", StringSplitOptions.RemoveEmptyEntries), Output: split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries));
}).ToList();

Console.WriteLine(signals.Sum(x => x.Output.Count(t => new int[] { 2, 3, 4, 7 }.Contains(t.Length))));
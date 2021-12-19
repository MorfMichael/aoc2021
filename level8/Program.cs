string[] input = File.ReadAllLines("input.txt");

List<(string[] Input, string[] Output)> signals = input.Select(t =>
{
    var split = t.Split(" | ");
    return (Input: split[0].Split(), Output: split[1].Split());
}).ToList();

var first = input[0];

Console.WriteLine(first);
//string[] input = File.ReadAllLines("example.txt");
string[] input = File.ReadAllLines("input.txt");

List<(List<string> Input, List<string> Output)> signals = input.Select(t =>
{
    var split = t.Split(" | ");
    return (Input: split[0].Split().Select(Order).ToList(), Output: split[1].Split().Select(Order).ToList());
}).ToList();

int sum = 0;

foreach (var signal in signals)
{
    Dictionary<string, string> digits = new Dictionary<string, string>();

    string one = signal.Input.First(x => x.Length == 2);
    string four = signal.Input.First(x => x.Length == 4);
    string seven = signal.Input.First(x => x.Length == 3);
    string eight = signal.Input.First(x => x.Length == 7);

    string nine = signal.Input.First(x => x.Length == 6 && four.All(f => x.Contains(f)) && seven.All(s => x.Contains(s)));
    string zero = signal.Input.First(x => x.Length == 6 && seven.All(s => x.Contains(s)) && !nine.Contains(x));
    string three = signal.Input.First(x => x.Length == 5 && seven.All(s => x.Contains(s)));
    string five = signal.Input.First(x => x.Length == 5 && x.All(f => nine.Contains(f)) && x != three);
    string six = signal.Input.First(x => x.Length == 6 && x != nine && x != zero);
    string two = signal.Input.First(x => x.Length == 5 && x != three && x != five);

    digits.Add(zero, "0");
    digits.Add(one, "1");
    digits.Add(two, "2");
    digits.Add(three, "3");
    digits.Add(four, "4");
    digits.Add(five, "5");
    digits.Add(six, "6");
    digits.Add(seven, "7");
    digits.Add(eight, "8");
    digits.Add(nine, "9");

    string output = string.Join(string.Empty, signal.Output.Select(t => digits[t]));

    Console.WriteLine(output);
    sum += int.Parse(output);
}

Console.WriteLine($"Summe: {sum}");

string Order(string input) => new string(input.OrderBy(t => t).ToArray());

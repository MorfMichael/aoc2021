//string[] input = File.ReadAllLines("input.txt");
string[] input = new string[] { "ebfg bfgdea gaf gf baedgc dafec cfdabg ecfabgd fdgea dbaeg | gaedbc egbf dbgcea dagfebc" };

List<(List<string> Input, List<string> Output)> signals = input.Select(t =>
{
    var split = t.Split(" | ");
    return (Input: split[0].Split().Select(Order).ToList(), Output: split[1].Split().Select(Order).ToList());
}).ToList();

var segments = new Dictionary<int, string>();

var signal = signals[0];

var one = signal.Input.First(x => x.Length == 2);
segments[2] = segments[5] = one;
var four = signal.Input.First(x => x.Length == 4);
segments[1] = segments[3] = new string(four.Except(one).ToArray());
var seven = signal.Input.First(x => x.Length == 3);
segments[0] = new string(seven.Except(one).ToArray());
var eight = signal.Input.First(x => x.Length == 7);
segments[4] = segments[6] = new string(eight.Except(four).Except(seven).ToArray());

// i dont know

string Order(string input) => new string(input.OrderBy(t => t).ToArray());

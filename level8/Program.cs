string[] input = File.ReadAllLines("input.txt");

List<(string[] Input, string[] Output)> signals = input.Select(t =>
{
    var split = t.Split(" | ");
    return (Input: split[0].Split(), Output: split[1].Split());
}).ToList();

var first = input[0];




Console.WriteLine(first);

class Model
{
    private readonly List<string> _input;
    private Dictionary<int, string> _digits;
    private Dictionary<int, string> _segments = new Dictionary<int, string>
    {
        { 0, string.Empty },
        { 1, string.Empty },
        { 2, string.Empty },
        { 3, string.Empty },
        { 4, string.Empty },
        { 5, string.Empty },
        { 6, string.Empty },
    };

    public Model(List<string> input)
    {
        _digits = new Dictionary<int, string>();
        _input = input.Select(Order).ToList();

        ParseInput();
    }
    
    private string Order(string input) => new string(input.OrderBy(t => t).ToArray());

    private void ParseInput()
    {
        var one = _input.First(x => x.Length == 2);
        _digits.Add(1, one);
        _segments[2] = _segments[5] = one;

        var four = _input.First(x => x.Length == 4);
        _digits.Add(4, four);
        string topleft_center = new string(four.Except(one).ToArray());
        _segments[1] = _segments[3] = topleft_center;

        var seven = _input.First(x => x.Length == 3);
        _digits.Add(7, seven);
        _segments[0] = new string(seven.Except(one).ToArray());

        var eight = _input.First(x => x.Length == 7);
        _digits.Add(8, eight);
        string bottomleft_bottom = new string(eight.Except(four).Except(seven).ToArray());
        _segments[4] = _segments[6] = bottomleft_bottom;

        string tmp_three = _segments[0] + _segments[2] + _segments[3] + _segments[5] + _segments[6];
        string tmp_two = _segments[0] + _segments[2] + _segments[3] + _segments[4] + _segments[6];

    }

    public int Decode(List<string> output)
    {
        output = output.Select(Order).ToList();

        return 1;
    }
}
//string[] lines = File.ReadAllLines("example.txt");
string[] lines = File.ReadAllLines("input.txt");

List<(char expected, char got)> illegal = new List<(char,char)>();

string close = ">)]}";
string open = "<([{";

Dictionary<char, int> values = new Dictionary<char, int>
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
};

foreach (var line in lines)
{
    string x = "";

    foreach (var c in line)
    {
        if (close.Contains(c))
        {
            char expected = close[open.IndexOf(x.Last())];
            if (c == expected)
            {
                x = x[..^1];
            }
            else
            {
                illegal.Add((expected,c));
                break;
            }
        }
        else
        {
            x += c;
        }
    }
}

int score = illegal.Sum(x => values[x.got]);
Console.WriteLine(score);
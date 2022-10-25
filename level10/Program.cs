//string[] lines = File.ReadAllLines("example.txt");
string[] lines = File.ReadAllLines("input.txt");

List<(char expected, char got)> illegal = new List<(char,char)>();
List<(string braces, ulong score)> uncompleted = new List<(string,ulong)>();

string close = ">)]}";
string open = "<([{";

Dictionary<char, int> failed_scores = new Dictionary<char, int>
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
};

Dictionary<char, ulong> uncomplete_scores = new Dictionary<char, ulong>()
{
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 }
};

foreach (var line in lines)
{
    string x = "";
    bool broke = false;

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
                broke = true;
                illegal.Add((expected,c));
                break;
            }
        }
        else
        {
            x += c;
        }
    }

    if (x.Length > 0 && !broke)
    {
        string uncomp = string.Empty;
        ulong sc = 0;
        foreach (var c in x.Reverse())
        {
            char cl = close[open.IndexOf(c)];
            uncomp += cl;
            sc = sc * 5 + uncomplete_scores[cl];
        }
        uncompleted.Add((uncomp,sc));
    }
}

int failed = illegal.Sum(x => failed_scores[x.got]);
uncompleted = uncompleted.OrderBy(t => t.score).ToList();
ulong uncomplet = uncompleted[uncompleted.Count / 2].score;
Console.WriteLine(uncomplet);
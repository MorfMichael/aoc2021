using System.IO;
using System.Linq;

string a = "";
string b = "";
var input = File.ReadAllLines("input.txt").SelectMany(x => x.Select((t,i) => (Index: i, Char: t))).GroupBy(x => x.Index, x => x.Char).ToList();

foreach (var index in input) 
{
    if (index.Count(x => x == '1') > index.Count(x => x == '0'))
    {
        a += '1';
        b += '0';
    }
    else 
    {
        a += '0';
        b += '1';
    }
}

int result = Convert.ToInt32(a,2) * Convert.ToInt32(b,2);
File.WriteAllText("output.txt", result.ToString());
Console.WriteLine(result);
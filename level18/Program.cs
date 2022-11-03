using System.Runtime.Intrinsics.X86;
using level18;

string input = File.ReadAllText("input.txt");

List<Node> nodes = input.Split(Environment.NewLine).Select(Node.Parse).ToList();

Part2();

void Part1()
{
    Node result = nodes[0];

    foreach (var node in nodes.Skip(1))
    {
        result = Node.Add(result, node);
        result.Reduce();
        Console.WriteLine(result);
    }

    Console.WriteLine(result.Magnitude);
}


void Part2()
{
    int max = 0;
    Node a1 = null, b1 = null, r = null;

    foreach (var a in nodes)
    {
        foreach (var b in nodes)
        {
            if (a == b) continue;

            var r1 = Node.Parse(a.ToString()).Add(Node.Parse(b.ToString()));
            r1.Reduce(false);
            var r2 = Node.Parse(b.ToString()).Add(Node.Parse(a.ToString()));
            r2.Reduce(false);

            if (r1.Magnitude > max)
            {
                max = r1.Magnitude;
                a1 = a;
                b1 = b;
                r = r1;
            }

            if (r2.Magnitude > max)
            {
                max = r2.Magnitude;
                a1 = b;
                b1 = a;
                r = r2;
            }
        }
    }

    Console.WriteLine("Result");
    Console.WriteLine($"{a1} + {b1}");
    Console.WriteLine(r);
    Console.WriteLine(max);
}

using level18;

string input = File.ReadAllText("input.txt");

List<Node> nodes = input.Split(Environment.NewLine).Select(Node.Parse).ToList();

Node result = nodes[0];

foreach (var node in nodes.Skip(1))
{
    result = Node.Add(result, node);
    result.Reduce();
    Console.WriteLine(result);
}

Console.WriteLine(result.Magnitude);


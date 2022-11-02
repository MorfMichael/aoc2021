using level18;

string input = @"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]";

List<Node> nodes = input.Split(Environment.NewLine).Select(Node.Parse).ToList();

Node result = nodes[0];

foreach (var node in nodes.Skip(1))
{
    result = Node.Add(result, node);
    result.Reduce();
    Console.WriteLine(result);
}


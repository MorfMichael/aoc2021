using level18;

string input = "[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]";
Node node = Node.Parse(input);
Console.WriteLine(node);

Node a = Node.Parse("[1,2]");
Node b = Node.Parse("[[3,4],5]");
Node result = a.Add(b);
result.Print();
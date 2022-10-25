namespace level18
{
    public class Node
    { 
        public int Depth => (Parent?.Depth ?? 0) + 1;

        public int? Value { get; set; }

        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node? Parent { get; set; }

        public bool Explode => Depth >= 4;

        public bool Split => Value.HasValue && Value.Value >= 10;

        public override string ToString() => Value.HasValue ? Value.Value.ToString() : $"[{Left},{Right}]";

        public static Node Parse(string input)
        {
            if (int.TryParse(input, out var parsed)) return new Node { Value = parsed };

            int d = 0;
            int split = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '[') d++;
                if (input[i] == ']') d--;
                if (input[i] == ',' && d == 1)
                {
                    split = i;
                    break;
                }
            }

            var parent = new Node();
            var left = Node.Parse(input[1..split]);
            var right = Node.Parse(input[(split + 1)..^1]);
            left.Parent = parent;
            right.Parent = parent;
            parent.Left = left;
            parent.Right = right;
            return parent;
        }

        public static Node Add(Node left, Node right) => new Node { Left = left, Right = right };
        public Node Add(Node right) => new Node { Left = this, Right = right };

        public void Print()
        {
            Console.WriteLine($"{this} - Depth: {Depth}");
            Left?.Print();
            Right?.Print();
        }
    }
}

using System.Xml.Linq;
using System.Xml.XPath;

namespace level18
{
    public enum NodeType
    {
        Value,
        Node
    }

    public class Node
    {
        public const string EXPLODE = "explode";
        public const string SPLIT = "split";

        public int Index { get; set; }

        public NodeType Type { get; set; }

        public int Depth => (Parent?.Depth ?? 0) + 1;

        public int? Value { get; set; }

        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node? Parent { get; set; }

        public bool ToExplode => Depth > 4;

        public bool ToSplit => Value.HasValue && Value.Value >= 10;

        public int Magnitude => Type == NodeType.Node ? (3 * (Left?.Magnitude ?? 0) + 2 * (Right?.Magnitude ?? 0)) : Value.Value;

        public override string ToString() => Value.HasValue ? Value.Value.ToString() : $"[{Left},{Right}]";

        public static Node Parse(string input)
        {
            if (int.TryParse(input, out var parsed)) return new Node { Value = parsed, Type = NodeType.Value };

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

            var left = Node.Parse(input[1..split]);
            var right = Node.Parse(input[(split + 1)..^1]);
            return Add(left, right);
        }

        public static Node Add(Node left, Node right)
        {
            var parent = new Node { Type = NodeType.Node };
            left.Parent = parent;
            right.Parent = parent;
            parent.Left = left;
            parent.Right = right;
            return parent;
        }
        public Node Add(Node right) => Add(this, right);

        public IEnumerable<Node> Flatten()
        {
            if (Type == NodeType.Value) yield break;

            yield return this;

            if (Left != null)
            {
                var left = Left.Flatten();
                foreach (var l in left) yield return l;
            };

            if (Right != null)
            {
                var right = Right.Flatten();
                foreach (var r in right) yield return r;
            }
        }

        public void Reduce(bool log = true)
        {
            var source = GetActions().Select((p,i) => new { Node = p.Node, Action = p.Action, Index = i }).ToList();
            var actions = new Stack<(Node Node,string Action)>(source.OrderBy(t => t.Action == EXPLODE ? 1 : 2).ThenBy(t => t.Index).Select(t => (t.Node,t.Action)).Reverse());

            while (actions.Any())
            {
                (Node node, string action) = actions.Pop();
                string messsage = $"after {action} {node}: ";
                if (action == EXPLODE) node.Explode();
                else if (action == SPLIT) node.Split();
                if (log) Console.WriteLine(messsage + this);
                source = GetActions().Select((p, i) => new { Node = p.Node, Action = p.Action, Index = i }).ToList();
                actions = new Stack<(Node Node, string Action)>(source.OrderBy(t => t.Action == EXPLODE ? 1 : 2).ThenBy(t => t.Index).Select(t => (t.Node, t.Action)).Reverse());
            }
        }

        private IEnumerable<(Node Node, string Action)> GetActions()
        {
            if (ToExplode && Type == NodeType.Node) yield return (this, EXPLODE);
            if (ToSplit && Type == NodeType.Value) yield return (this, SPLIT);

            var left = Left?.GetActions() ?? Enumerable.Empty<(Node,string)>();
            var right = Right?.GetActions() ?? Enumerable.Empty<(Node, string)>();
            foreach (var action in left.Concat(right)) yield return action;
        }

        private void Split()
        {
            if (!ToSplit) return;

            int left = Value.Value / 2;
            int right = (int)((Value.Value / 2d) + .5);
            Left = new Node { Value = left, Parent = this, Type = NodeType.Value };
            Right = new Node { Value = right, Parent = this, Type = NodeType.Value };

            Value = null;
            Type = NodeType.Node;
        }

        private void Explode()
        {
            if (!ToExplode || Type == NodeType.Value)
            {
                return;
            }

            Node left = GetLeftNeighbour();
            if (left != null)
            {
                left.Value += Left.Value;
            }

            Node right = GetRightNeighbour();
            if (right != null)
            {
                right.Value += Right.Value;
            }

            this.Value = 0;
            this.Left = null;
            this.Right = null;
            this.Type = NodeType.Value;
        }

        private Node GetLeftNeighbour()
        {
            bool moved = false;

            var node = this;
            while (node != null && node.Type != NodeType.Value)
            {
                if (moved)
                {
                    node = node.Right;
                    continue;
                }

                if (node.Parent?.Left != null && node.Parent.Left != node)
                {
                    node = node.Parent.Left;
                    moved = true;
                }
                else
                {
                    node = node.Parent;
                }
            }

            return node;
        }

        private Node GetRightNeighbour()
        {
            bool moved = false;
            var node = this;
            while (node != null && node.Type != NodeType.Value)
            {
                if (moved)
                {
                    node = node.Left;
                    continue;
                }

                if (node.Parent?.Right != null && node.Parent.Right != node)
                {
                    node = node.Parent.Right;
                    moved = true;
                }
                else
                {
                    node = node.Parent;
                }
            }

            return node;
        }
    }
}
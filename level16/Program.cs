string input = File.ReadAllText("input.txt");
string binary = string.Join(string.Empty, input.Select(ch => Convert.ToString(Convert.ToInt32(ch.ToString(), 16), 2).PadLeft(4, '0')));

Packet p = Packet.Parse(binary);

abstract class Packet
{

    internal string _input;
    public int Version { get; set; }
    public abstract PacketType Type { get; }
    public int Length { get; set; }

    private ulong _value;
    public ulong Value 
    { 
        get => _value;
        set
        {
            _value = value;
            Console.WriteLine(value);
        }
    }

    public Packet(string input)
    {
        _input = input;
        Version = Convert.ToInt32(input[0..3], 2);
        Console.WriteLine(Type);
    }

    public static Packet Parse(string input)
    {
        string s_version = input[0..3];
        string s_type = input[3..6];
        int type = Convert.ToInt32(s_type, 2);
        switch (type)
        {
            case 0: return new SumPacket(input);
            case 1: return new ProductPacket(input);
            case 2: return new MinimumPacket(input);
            case 3: return new MaximumPacket(input);
            case 4: return new LiteralPacket(input);
            case 5: return new GreaterPacket(input);
            case 6: return new LessPacket(input);
            case 7: return new EqualPacket(input);
            default: return null;
        }
    }

    public IEnumerable<Packet> Flatten()
    {
        yield return this;

        if (this is OperatorPacket op)
        {
            foreach (var ch in op.Children.SelectMany(t => t.Flatten()))
            {
                yield return ch;
            }
        }
    }
}

class LiteralPacket : Packet
{
    public LiteralPacket(string input) : base(input)
    {
        string content = input[6..];
        string current = content[0..5];
        string value = string.Empty;
        int length = 6;
        while (content.Length > 0)
        {
            value += current[1..];
            length += current.Length;

            if (current[0] == '0') break;

            content = content[5..];
            current = content[0..5];
        }

        Value = Convert.ToUInt64(value, 2);
        Length = length;
    }

    public override PacketType Type => PacketType.Literal;
}

abstract class OperatorPacket : Packet
{
    public OperatorPacket(string input) : base(input)
    {
        if (_input[6] == '0')
        {
            string s = _input[7..22];
            int bitLength = Convert.ToInt32(s, 2);

            string next = _input[22..(22 + bitLength)];

            Length = 22 + bitLength;

            int count = 0;

            while (count < bitLength)
            {
                Packet p = Packet.Parse(next);
                count += p.Length;
                Children.Add(p);
                next = next[p.Length..];
            }
        }
        else 
        {
            string s = _input[7..18];
            int packetCount = Convert.ToInt32(s, 2);

            string rest = _input[18..];

            while (Children.Count < packetCount)
            {
                var p = Packet.Parse(rest);
                Children.Add(p);
                rest = rest[p.Length..];
            }

            Length = 18 + Children.Sum(x => x.Length);
        }

        Value = DoCalculation();
    }

    public List<Packet> Children { get; set; } = new List<Packet>();

    public abstract ulong DoCalculation();
}

class SumPacket : OperatorPacket
{
    public SumPacket(string input) : base(input) { }

    public override PacketType Type => PacketType.Sum;

    public override ulong DoCalculation() => Children.Aggregate(0UL, (a,b) => a + b.Value);
}

class ProductPacket : OperatorPacket
{
    public ProductPacket(string input) : base(input) { }

    public override PacketType Type => PacketType.Product;

    public override ulong DoCalculation() =>  Children.Aggregate<Packet, ulong>(1, (acc, child) => acc * child.Value);
}

class MinimumPacket : OperatorPacket
{
    public MinimumPacket(string input) : base(input) { }

    public override PacketType Type => PacketType.Minimum;

    public override ulong DoCalculation() => Children.Min(x => x.Value);
}

class MaximumPacket : OperatorPacket
{
    public MaximumPacket(string input) : base(input) { }

    public override PacketType Type => PacketType.Maximum;

    public override ulong DoCalculation() => Children.Max(x => x.Value);
}

class GreaterPacket : OperatorPacket
{
    public GreaterPacket(string input) : base(input) { }

    public override PacketType Type => PacketType.Greater;

    public override ulong DoCalculation() => Children.First().Value > Children.Last().Value ? (ulong)1 : (ulong)0;
}

class LessPacket : OperatorPacket
{
    public LessPacket(string input) : base(input) { }

    public override PacketType Type => PacketType.Less;

    public override ulong DoCalculation() => Children.First().Value < Children.Last().Value ? (ulong)1 : (ulong)0;
}

class EqualPacket : OperatorPacket
{
    public EqualPacket(string input) : base(input) { }

    public override PacketType Type => PacketType.Equal;

    public override ulong DoCalculation() => Children.First().Value == Children.Last().Value ? (ulong)1 : (ulong)0;
}

public enum PacketType
{
    Sum,
    Product,
    Minimum,
    Maximum,
    Literal,
    Greater,
    Less,
    Equal
}
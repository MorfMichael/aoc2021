string input = "D2FE28";

string data = string.Join(string.Empty, input.Select(ch => Convert.ToString(Convert.ToInt32(ch.ToString(), 16), 2).PadLeft(4, '0')));


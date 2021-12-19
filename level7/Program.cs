string input = File.ReadAllText("input.txt");
List<int> ships = input.Split(",").Select(t => int.Parse(t)).ToList();

int min = ships.Min();
int max = ships.Max();

Console.WriteLine($"Ships: {ships.Count}");
Console.WriteLine($"Min: {min}, Max: {max}");

Dictionary<int, int> SumOfPositions = new Dictionary<int, int>();

foreach (var ship in ships)
{
    for (int i = min; i < max; i++)
    {
        int cur = Enumerable.Range(1, Math.Abs(ship - i)).Sum();
        if (SumOfPositions.ContainsKey(i)) SumOfPositions[i] += cur;
        else SumOfPositions.Add(i, cur);
    }
}

int pos = SumOfPositions.MinBy(x => x.Value).Key;

Console.WriteLine(SumOfPositions[pos]);
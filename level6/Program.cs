string input = File.ReadAllText("input.txt");
List<int> fishies = input.Split(",").Select(t => int.Parse(t)).ToList();
int days = 256;

long sum = fishies.Sum(x => Calculate(x, days));
Console.WriteLine(sum);

long Calculate(int start, int days)
{
    long result = 0;

    for (int i = 0; i < days; i++)
    {
        if (start > 0)
        {
            start--;
        }
        else
        {
            start = 6;
            result++;
            result += Calculate(8, days - i - 1);
        }
    }

    return result;
}

Console.WriteLine(fishies.Count);
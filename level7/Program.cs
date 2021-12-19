string input = File.ReadAllText("input.txt");
List<int> positions = input.Split(",").Select(t => int.Parse(t)).ToList();

int max = positions.Max();
int minSum = 100000000;
for (int i = 0; i < max; i++)
{
    int sum = positions.Sum(x => Calculate(x, i));
    if (sum < minSum)
        minSum = sum;
}

Console.WriteLine(minSum);

int Calculate(int current, int target) => Math.Abs(current - target);
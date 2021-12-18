string input = File.ReadAllText("input.txt");
List<int> fishies = input.Split(",").Select(t => int.Parse(t)).ToList();
int days = 256;

for (int day = 0; day < days; day++)
{
    for (int i = 0; i < fishies.Count; i++)
    {
        if (fishies[i] > 0)
        {
            fishies[i]--;
        }
        else
        {
            fishies[i] = 6;
            fishies.Add(9);
        }
    }
}

Console.WriteLine(fishies.Count);
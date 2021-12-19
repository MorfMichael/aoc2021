//string input = File.ReadAllText("input.txt");
//List<int> fishis = input.Split(",").Select(t => int.Parse(t)).ToList();

int days = 256;

List<int> fishis = new List<int> { 3, 4, 3, 1, 2 };

for (int day = 1; day <= days; day++)
{
    int before = fishis.Count;
    for (int i = 0; i < fishis.Count; i++)
    {
        if (fishis[i] > 0)
        {
            fishis[i]--;
        }
        else
        {
            fishis[i] = 6;
            fishis.Add(9);
        }
    }

    Console.WriteLine($"day {day.ToString().PadLeft(3)}: fishies ({before}) and babies ({fishis.Count - before}) results a totoal of {fishis.Count}");
}
Console.WriteLine($"There will be {fishis.Count} fishies after {days} days!");

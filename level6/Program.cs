string input = File.ReadAllText("input.txt");
List<int> fishis = input.Split(",").Select(t => int.Parse(t)).ToList();
int days = 256;

List<int> toAdd = new List<int>();

Console.WriteLine($"initial state: {string.Join(",", fishis)}");

for (int day = 1; day <= days; day++)
{
    for (int i = 0; i < fishis.Count; i++)
    {
        if (fishis[i] > 0)
        {
            fishis[i]--;
        }
        else
        {
            fishis[i] = 6;
            toAdd.Add(8);
        }
    }

    fishis.AddRange(toAdd);
    toAdd.Clear();

    //Console.WriteLine($"After {day.ToString().PadLeft(2)} days: {string.Join(",", fishis)}");

}

Console.WriteLine(fishis.Count);

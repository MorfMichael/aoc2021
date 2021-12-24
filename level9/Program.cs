string[] lines = File.ReadAllLines("input.txt");
//string[] lines = File.ReadAllLines("example.txt");

int[][] map = lines.Select(t => t.Select(x => int.Parse(x.ToString())).ToArray()).ToArray();

List<int> lowest = new List<int>();


for (int i = 0; i < map.Length; i++)
{
    for (int j = 0; j < map[i].Length; j++)
    {
        int value = map[i][j];

        if (i == 0)
        {
            if (j == 0)
            {
                if (map[i + 1][j] > value && map[i][j+1]> value)
                    lowest.Add(value);
            }
            else if (j == map[i].Length - 1)
            {
                if (map[i + 1][j] > value && map[i][j - 1] > value)
                    lowest.Add(value);
            }
            else
            {
                if (map[i + 1][j] > value && map[i][j - 1] > value && map[i][j + 1] > value)
                    lowest.Add(value);
            }
        }
        else if (i == map.Length - 1)
        {
            if (j == 0)
            {
                if (map[i - 1][j] > value && map[i][j + 1] > value)
                    lowest.Add(value);
            }
            else if (j == map[i].Length - 1)
            {
                if (map[i - 1][j] > value && map[i][j - 1] > value)
                    lowest.Add(value);
            }
            else
            {
                if (map[i - 1][j] > value && map[i][j - 1] > value && map[i][j + 1] > value)
                    lowest.Add(value);
            }
        }
        else
        {
            if (j == 0)
            {
                if (map[i - 1][j] > value && map[i+1][j] > value && map[i][j + 1] > value)
                    lowest.Add(value);
            }
            else if (j == map[i].Length - 1)
            {
                if (map[i - 1][j] > value && map[i + 1][j] > value && map[i][j - 1] > value)
                    lowest.Add(value);
            }
            else
            {
                if (map[i - 1][j] > value && map[i+1][j] > value && map[i][j - 1] > value && map[i][j + 1] > value)
                    lowest.Add(value);
            }
        }
    }
}

int result = lowest.Sum(x => 1 + x);
Console.WriteLine(result);
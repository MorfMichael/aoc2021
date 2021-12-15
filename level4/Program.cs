using System.Collections.Concurrent;

(int, int)? CheckValue(int[,] board, int value)
{
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (board[i, j] == value)
            {
                return (i, j);
            }
        }
    }

    return null;
}

string[] lines = File.ReadAllLines("input.txt");

int[] numbers = lines[0].Split(",").Select(t => int.Parse(t)).ToArray();

Dictionary<int, int[,]> boards = new Dictionary<int, int[,]>();

var left = lines.Skip(2);
int id = 1;
while (left.Any())
{
    int[,] board = new int[5, 5];
    int y = 0, x = 0;
    var data = left.Take(5).Select(t => t.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray());
    foreach (var row in data)
    {
        x = 0;
        foreach (var value in row)
        {
            board[y, x++] = value;
        }
        y++;
    }

    boards.Add(id++, board);

    left = left.Skip(6);
}

ConcurrentDictionary<int, List<(int, int)>> result = new ConcurrentDictionary<int, List<(int, int)>>();

int sum = 0;
int num = 0;

foreach (var number in numbers)
{
    foreach (var board in boards)
    {
        var check = CheckValue(board.Value, number);

        if (check != null)
        {
            result.AddOrUpdate(board.Key, new List<(int, int)> { check.Value }, (id, y) => { y.Add(check.Value); return y; });
        }
    }

    int win =
        result.Where(x => x.Value.GroupBy(t => t.Item1).Any(t => t.Count() == 5) || 
        x.Value.GroupBy(t => t.Item2).Any(t => t.Count() == 5)).Select(t => t.Key).FirstOrDefault();
    if (win > 0)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (!result[win].Contains((i, j))) 
                    sum += boards[win][i, j];
            }
        }

        num = number;
        break;
    }
}

Console.WriteLine(num * sum);


using System.Security.Authentication.ExtendedProtection;

namespace level19
{
    public class Scanner
    {
        public int Id { get; set; }

        public List<Point> Points { get; set; } = new();
        public List<(Point start, Point end, double distance)> Distances { get; set; } = new();

        public Scanner(string[] lines)
        {
            foreach (var line in lines)
            {
                if (line.StartsWith("---"))
                {
                    string id = line.Replace("--- scanner ", "").Replace("---", "").Trim();
                    Id = int.Parse(id);
                }
                else
                {
                    Points.Add(new Point(line));
                }
            }
        }

        public void CalculateDistances()
        {
            Distances.Clear();

            foreach (var start in Points)
            {
                foreach (var end in Points)
                {
                    if (start == end || Distances.Any(t => t.start == start && t.end == end || (t.start == end && t.end == start))) continue;

                    Distances.Add((start, end, start.Distance(end)));
                }
            }
        }

        public void PrintDistances()
        {
            Console.WriteLine($" --- scanner {Id} --- ");
            Console.WriteLine(string.Join(Environment.NewLine, Distances.OrderBy(t => t.distance).Select(t => $"{t.distance}")));
        }
    }
}

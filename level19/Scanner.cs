using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;

namespace level19
{
    public class Scanner
    {
        public int Id { get; set; }

        public List<Point> Points { get; set; } = new();
        public List<Distance> Distances { get; set; } = new();

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
                    if (start == end || Distances.Any(t => (t.Start == start && t.End == end) || (t.Start == end && t.End == start))) continue;

                    Distances.Add(new Distance(start, end));
                }
            }
        }

        public List<(Distance first, Distance second)> CompareDistances(Scanner scanner)
        {
            List<(Distance first, Distance second)> result = new List<(Distance first, Distance second)>();

            foreach (var d in Distances)
            {
                var other = scanner.Distances.FirstOrDefault(x => x.Value == d.Value);
                if (other != null)
                {
                    result.Add((first: d, second: other));
                }
            }

            return result;
        }

        public void PrintDistances()
        {
            Console.WriteLine($" --- scanner {Id} --- ");
            Console.WriteLine(string.Join(Environment.NewLine, Distances.OrderBy(t => t.Value).Select(t => $"{t.Value}")));
        }
    }
}

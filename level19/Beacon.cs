using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace level19
{
    public class Beacon
    {
        private List<(double, Vector3, Beacon)> _distances = new();

        public Beacon(Scanner2 scanner, string input)
        {
            Scanner = scanner;

            int[] parse = input.Split(",").Select(int.Parse).ToArray();
            Position = new Vector3(parse[0], parse[1], parse[2]);
        }

        public Scanner2 Scanner { get; set; }

        public Vector3 Position { get; set; }

        public List<Beacon> Other { get; set; } = new();

        public IEnumerable<(double, Vector3, Beacon)> Distances => _distances;

        public void CalculateDistances(params Beacon[] beacons)
        {
            var distances = new List<(double, Vector3, Beacon)>();

            foreach (var beacon in beacons)
            {
                if (this == beacon) continue;

                double distance = Vector3.Distance(Position, beacon.Position);

                if (!distances.Any(t => t.Item1 == distance || t.Item3.Position == beacon.Position))
                {
                    distances.Add((distance, beacon.Position - Position, beacon));
                    Console.WriteLine($"{Scanner.Id} | from: {Position} to: {beacon.Position} result: {distance}");
                }
            }

            _distances = distances;
        }

    }
}

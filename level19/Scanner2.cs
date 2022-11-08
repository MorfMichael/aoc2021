using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace level19
{
    public class Scanner2
    {
        public Scanner2(string[] lines)
        {
            Beacons = new List<Beacon>();

            foreach (var line in lines)
            {
                if (line.StartsWith("---"))
                {
                    string id = line.Replace("--- scanner ", "").Replace("---", "").Trim();
                    Id = int.Parse(id);
                }
                else
                {
                    Beacons.Add(new Beacon(this, line));
                }
            }

            foreach (var b1 in Beacons)
            {
                b1.CalculateDistances(Beacons.ToArray());
            }
        }

        public int Id { get; set; }

        public List<Beacon> Beacons { get; set; }
    }
}

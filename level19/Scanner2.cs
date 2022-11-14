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
        }

        public int Id { get; set; }

        public List<Beacon> Beacons { get; set; }

        public IEnumerable<Vector3> Rotate(Quaternion rotation)
        {
            foreach (var beacon in Beacons)
            {
                yield return Vector3.Transform(beacon.Position, rotation);
            }
        }

        public void Normalize(Quaternion rotation, Vector3 distance)
        {
            foreach (var beacon in Beacons)
            {
                beacon.Position = Vector3.Transform(beacon.Position, rotation).Round();
                beacon.Position = (beacon.Position - distance).Round();
            }
        }
    }
}

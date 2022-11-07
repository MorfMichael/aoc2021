using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace level19
{
    public class Distance
    {
        public Distance(Point start, Point end)
        {
            Start = start;
            End = end;

            Diff = end - start;

            Value = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2) + Math.Pow(end.Z - start.Z, 2));
        }

        public Point Start { get; set; }

        public Point End { get; set; }

        public double Value { get; set; }

        public Point Diff { get; set; }
    }
}

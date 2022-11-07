using System.Runtime.CompilerServices;

namespace level19
{
    public class Point
    {
        public Point(string input)
        {
            int[] parsed = input.Split(",").Select(int.Parse).ToArray();
            X = parsed[0];
            Y = parsed[1];
            Z = parsed[2];
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public double Distance(Point point) => Math.Abs(Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2) + Math.Pow(Z - point.Z, 2)));

        public override string ToString() => $"({X},{Y},{Z})";

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Point p) return this == p;

            return false;
        }
    }
}

namespace Kontur.Sokoban.Models
{
    public class Vec
    {
        public readonly int X, Y;

        public Vec(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec other)
            {
                return Equals(other);
            }
            return false;
        }

        private bool Equals(Vec other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return 997 * X ^ Y;
        }
    }
}
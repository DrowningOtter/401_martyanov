namespace GenAlgo
{
    public class Arrangement(List<Square> array)
    {
        public List<Square> Lst { get; set; } = array;
        public double CalcCoverageArea()
        {
            int maxX = Lst.Max(sq => sq.X + sq.Size);
            int minX = Lst.Min(sq => sq.X);
            int maxY = Lst.Max(sq => sq.Y + sq.Size);
            int minY = Lst.Min(sq => sq.Y);
            return (maxX - minX) * (maxY - minY);
        }
        private static bool CheckCollision(Square a, Square b)
        {
            return a.X + a.Size > b.X && b.X + b.Size > a.X &&
                     a.Y + a.Size > b.Y && b.Y + b.Size > a.Y;
        }
        public bool HaveCollisions()
        {
            for (int i = 0; i < Lst.Count; i++)
            {
                for (int j = i + 1; j < Lst.Count; j++)
                {
                    if (CheckCollision(Lst[i], Lst[j]))
                        return true;
                }
            }
            return false;
        }
    }
}

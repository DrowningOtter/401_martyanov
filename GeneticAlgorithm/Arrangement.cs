using System.Runtime.CompilerServices;

namespace GenAlgo
{
    public class Arrangement(List<Square> array)
    {
        public List<Square> Lst { get; set; } = array;
        private static Random rnd = new Random();
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
        public static Arrangement GetRandomArrangement(int[] amounts)
        {
            var tryCount = 0;
            while (true) {
                int getRandom()
                {
                    var left = -10;
                    var right = 10;
                    return rnd.Next(1) * (right - left) + left;
                }
                var squaresList = new List<Square>();
                for (int j = 0; j < amounts.Length; ++j) {
                    squaresList.AddRange(
                        Enumerable.Range(0, amounts[j])
                        .Select(s => new Square(getRandom(), getRandom(), j + 1)));
                }
                var arrangement = new Arrangement(squaresList);
                if (!arrangement.HaveCollisions()) {
                    return arrangement;
                }
                if (tryCount > 300)
                {
                    return new Arrangement([]);
                }
                tryCount++;
            }
        }
        public override string ToString()
        {
            return string.Join("\n", Enumerable.Range(0, Lst.Count())
            .Select((i)=>Lst[i].ToString()).ToArray());
        }
    }
}

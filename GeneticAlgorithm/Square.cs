namespace GenAlgo
{
    public class Square(int x, int y, int size)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int Size { get; set; } = size;
        public Square Clone() {
            return new Square(this.X, this.Y, this.Size);
        }
    };
}
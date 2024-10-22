using GenAlgo;

public class Program {
    public static void Main() {
        var algo = new Algorithm();
        algo.CreateRandomPopulation(100, [2, 2, 1]);
        algo.StartEvolution(100);
        Arrangement bestArrangement = algo.BestArrangement;
        Console.WriteLine("solution found:");
        foreach (var sq in bestArrangement.Lst) {
            Console.WriteLine($"square: side length: {sq.Size}, node coords: [{sq.X},{sq.Y}]");
        }
        Console.WriteLine($"Area of found solution: {bestArrangement.CalcCoverageArea()}");
    }
}
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GenAlgo
{
    public class Algorithm : INotifyPropertyChanged
    {
        private List<Arrangement> population = [];
        private readonly int randMin = -10;
        private readonly int randMax = 10;
        private readonly Random rnd = new Random();
        public int Scale { get; set; }
        private double _curBestArea;
        public double CurrentBestArea
        {
            get { return _curBestArea; }
            set
            {
                if (_curBestArea != value)
                {
                    _curBestArea = value;
                    OnPropertyChanged();
                }
            }
        }
        private Arrangement _instantArrangement = new Arrangement([]);
        public Arrangement InstantArrangement
        {
            get { return _instantArrangement.Clone(); }
            set
            {
                _instantArrangement = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int getRandom() {
            return (int)((rnd.NextDouble() * (randMax - randMin) + randMin)*Scale);
        }

        public void CreateRandomPopulation(int populationSize, int[] amounts) {
            Console.WriteLine($"creating random population with {populationSize} size");
            for (int i = 0; i < populationSize;) {
                var squaresList = new List<Square>();
                for (int j = 0; j < amounts.Length; ++j) {
                    squaresList.AddRange(
                        Enumerable.Range(0, amounts[j])
                        .Select(s => new Square(getRandom(), getRandom(), (j + 1) * Scale)));
                }
                var arrangement = new Arrangement(squaresList);
                if (!arrangement.HaveCollisions()) {
                    i++;
                    population.Add(arrangement);
                }
            }
        }
        private Arrangement Crossover(Arrangement ar1, Arrangement ar2) {
            var crossovered = new List<Square>();
            if (ar1.Lst.Count != ar2.Lst.Count)
                throw new Exception("sizes of arrangement differs");
            var delimIndex = rnd.Next(1, ar1.Lst.Count-1);
            return new Arrangement(Enumerable.Concat(
            Enumerable.Range(0, delimIndex).Select(i=>ar1.Lst[i].Clone()),
            Enumerable.Range(delimIndex, ar2.Lst.Count - delimIndex).Select(i=>ar2.Lst[i].Clone())
            ).ToList());
        }
        private void Mutate(Arrangement arr) {
            Parallel.For(0, arr.Lst.Count, i => MutateOne(arr.Lst[i]));
        }
        private void MutateOne(Square sq)
        {
            if (rnd.NextDouble() < 0.2) {
                sq.X += rnd.Next(-1, 2) * Scale;
                sq.Y += rnd.Next(-1, 2) * Scale;
            }
        }

        private void EvolvePopulation() {
            var bestArrangements = population.OrderBy(s => s.CalcCoverageArea()).Take(population.Count() / 2).ToList();
            var newPopulation = new ConcurrentBag<Arrangement>();
            var tasks = new Task[population.Count];
            for (int i = 0; i < population.Count; i++)
            {
                tasks[i] = Task.Run(() => {
                    while (true)
                    {
                        var child = Crossover(bestArrangements[rnd.Next(bestArrangements.Count)], bestArrangements[rnd.Next(bestArrangements.Count)]);
                        Mutate(child);
                        if (!child.HaveCollisions())
                        {
                            newPopulation.Add(child);
                            break;
                        }
                    }
                });
            }
            Task.WaitAll(tasks);
            population = newPopulation.ToList();
        }

        public void StartEvolution(int maxGenerations, CancellationToken token) {
            var generation = 0;
            while (true) {
                if (token.IsCancellationRequested)
                    break;
                var bestArrangement = population.OrderBy(arr => arr.CalcCoverageArea()).First();
                Console.WriteLine($"#{generation} generation, best area is {bestArrangement.CalcCoverageArea()}");
                InstantArrangement = bestArrangement.Clone();
                CurrentBestArea = bestArrangement.CalcCoverageArea();
                EvolvePopulation();
                generation++;
            }
        }
    }
}
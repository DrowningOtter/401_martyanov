using System.ComponentModel;
using GenAlgo;
using System.Runtime.CompilerServices;
using System.Windows;

namespace OptimalSquaresUI;

public class ArrangementViewModel : INotifyPropertyChanged
{
    public bool IsCurrentlyRunning = false;
    public CancellationTokenSource? workStopper;
    private Algorithm? algo;
    public AlgoContext db;
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private double _curBestArea;
    public double CurBestArea
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
    private void UpdateCurBestArea(object? sender, PropertyChangedEventArgs e)
    {
        if (algo == null) return;
        if (e.PropertyName == nameof(algo.CurrentBestArea))
            CurBestArea = algo.CurrentBestArea;
    }
    private int _populationSize;
    public int PopulationSize
    {
        get { return _populationSize; }
        set
        {
            if (_populationSize != value)
            {
                _populationSize = value;
                OnPropertyChanged();
            }
        }
    }
    public Arrangement InstantArrangement
    {
        get { return _instantArr.Clone(); }
        set
        {
            _instantArr = value;
            OnPropertyChanged();
        }
    }
    private Arrangement _instantArr = new Arrangement([]);
    public void UpdateInstantArrangement(object? sender, PropertyChangedEventArgs e)
    {
        if (algo == null) return;
        if (e.PropertyName == nameof(algo.InstantArrangement))
            InstantArrangement = algo.InstantArrangement;
    }
    // default value
    public int[] Amounts = [2, 2, 1];
    private int _maxGenerations;
    public int MaxGenerations
    {
        get {return _maxGenerations; }
        set
        {
            if (_maxGenerations != value)
            {
                _maxGenerations = value;
                OnPropertyChanged();
            }
        }
    }
    public ArrangementViewModel()
    {
        db = new AlgoContext();
        PopulationSize = 100;
        MaxGenerations = 100;
    }

    public void SavePopulation(string name)
    {
        if (algo == null || IsCurrentlyRunning)
        {
            return;
        }
        var pop = new Population{Name = name};
        pop.Arrs = algo.Population;
        db.Populations.Add(pop);
        db.SaveChanges();
    }

    public string[] GetPopulationsNames()
    {
        var names = db.Populations.Select(p => p.Name).ToArray();
        return names;
    }

    public void LoadPopulation(string name)
    {
        if (this.algo == null)
            this.algo = new Algorithm();
        var pop = db.Populations.Select(p => p).Where(p => p.Name == name).First();
        pop.Arrs = db.Arrangements.Select(p => p).Where(a => a.Population.Id == pop.Id).ToList();
        foreach (var ar in pop.Arrs)
        {
            ar.Lst = db.Sqaures.Select(s => s).Where(s => s.Arrangement.ArrangementId == ar.ArrangementId).ToList();
        }
        this.algo.Population = pop.Arrs;
        InstantArrangement = pop.Arrs.OrderBy(p => p.CalcCoverageArea()).First();
    }

    public bool CanStartWork()
    {
        return !IsCurrentlyRunning && algo != null && algo.Population.Count > 0;
    }

    public void StopWork()
    {
        if (algo == null || workStopper == null)
            throw new Exception("work has not been started yet");
        workStopper.Cancel();
        workStopper.Dispose();
        IsCurrentlyRunning = false;
    }
    public void ResetAlgo()
    {
        algo = null;
        InstantArrangement = new Arrangement();
        CurBestArea = 0;
    }
    public void StartWork()
    {
        IsCurrentlyRunning = true;
        if (workStopper != null)
        {
            workStopper.Dispose();
        }
        workStopper = new CancellationTokenSource();
        if (algo == null)
            algo = new Algorithm{Scale = 1};
        algo.PropertyChanged += UpdateCurBestArea;
        algo.PropertyChanged += UpdateInstantArrangement;
        var size = algo.Population.Count;
        if (size == 0)
        {
            algo.CreateRandomPopulation(PopulationSize, Amounts);
        }
        CurBestArea = algo.CurrentBestArea;
        algo.StartEvolution(MaxGenerations, workStopper.Token);
    }
}
using System.ComponentModel;
using GenAlgo;
using System.Runtime.CompilerServices;

namespace OptimalSquaresUI;

public class ArrangementViewModel : INotifyPropertyChanged
{
    private Arrangement? _arr;
    public bool IsCurrentlyRunning = false;
    public CancellationTokenSource? workStopper;
    private Algorithm? algo;
    public Arrangement? Arr
    {
        get { return _arr; }
        set
        {
            _arr = value;
            OnPropertyChanged();
        }
    }
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
        PopulationSize = 100;
        MaxGenerations = 100;
    }

    public void StopWork()
    {
        if (algo == null || workStopper == null)
            throw new Exception("work has not been started yet");
        workStopper.Cancel();
        Arr = algo.BestArrangement;
        IsCurrentlyRunning = false;
    }
    public void StartWork()
    {
        IsCurrentlyRunning = true;
        workStopper = new CancellationTokenSource();
        algo = new Algorithm{Scale = 1};
        CurBestArea = 0;
        algo.PropertyChanged += UpdateCurBestArea;
        algo.CreateRandomPopulation(PopulationSize, Amounts);
        algo.StartEvolution(MaxGenerations, workStopper.Token);
    }
}
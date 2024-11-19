using System.ComponentModel;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GenAlgo;

namespace OptimalSquaresUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Rectangle[] drawedSquares;
    Line[] ?drawedLines;
    private int scale = 30;
    private ArrangementViewModel viewModel = new ArrangementViewModel();
    public MainWindow()
    {
        InitializeComponent();

        drawedSquares = [];

        this.DataContext = viewModel;

        viewModel.PropertyChanged += ViewModelInstantArrangementUpdated;

        SizeChanged += RenewArrEventHandeWrapper;
    }

    private void AddSquare(GenAlgo.Square square, int shiftX = 0, int shiftY = 0) {
        var drawRect = new Rectangle() {
            Width = square.Size * scale,
            Height = square.Size * scale,
            Stroke = Brushes.Black,
            StrokeThickness = 0.5,
            // Fill = Brushes.Black,
        };
        Canvas.SetLeft(drawRect, square.X * scale + canvas.ActualWidth / 2 - shiftX * scale);
        Canvas.SetTop(drawRect, square.Y * scale + canvas.ActualHeight / 2 - shiftY * scale);

        canvas.Children.Add(drawRect);
        drawedSquares = [.. drawedSquares, drawRect];
    }
    private void LoadArrangement(Arrangement? arr)
    {
        if (arr == null || arr.Lst.Count == 0)
        {
            return;
        }
        var shiftX = arr.Lst.Min(sq => sq.X) + arr.Lst.Max(sq => sq.X + sq.Size) / 2;
        var shiftY = arr.Lst.Min(sq => sq.Y) + arr.Lst.Max(sq => sq.Y + sq.Size) / 2;
        foreach (var square in arr.Lst)
        {
            AddSquare(square, shiftX, shiftY);
        }
    }
    private void RenewArrEventHandeWrapper(object sender, RoutedEventArgs e)
    {
        RenewArr(viewModel.InstantArrangement);
    }
    private void RenewArr(Arrangement? arr)
    {
        // recalc scale
        var scaleFactor = Math.Min(canvas.ActualHeight, canvas.ActualWidth);
        scale = (int)(scaleFactor / 20);
        for (var i = 0; i < drawedSquares.Length;i++)
        {
            canvas.Children.Remove(drawedSquares[i]);
        }
        drawedSquares = [];
        LoadArrangement(arr);
    }
    private void ViewModelInstantArrangementUpdated(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(viewModel.InstantArrangement))
            Dispatcher.Invoke(() => {RenewArr(viewModel.InstantArrangement);});
    }

    private int[] _amounts = [2, 2, 1];
    private void IsStartCalculationsAllowed(object sender, CanExecuteRoutedEventArgs e)
    {
        if (viewModel.CanStartWork())
        {
            e.CanExecute = true;
            return;
        }
        bool isCorrectAmounts = true;
        // check if array with amounts is correct
        try
        {
            _amounts = squareAmounts.Text.Split(' ').Select(int.Parse).ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Некорректные данные в поле Amounts{ex}");
            isCorrectAmounts = false;
        }
        
        e.CanExecute = !viewModel.IsCurrentlyRunning && isCorrectAmounts;
    }
    private void CalculationsStarted(object sender, ExecutedRoutedEventArgs e)
    {
        viewModel.Amounts = _amounts;
        Task.Run(() => viewModel.StartWork());
    }
    private void CanStopCalculations(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = viewModel.IsCurrentlyRunning;
    }
    private void CalculationsStopped(object sender, ExecutedRoutedEventArgs e)
    {
        // stop the running calculations here
        viewModel.StopWork();
    }
    private void OpenSaveWindow(object sender, RoutedEventArgs e)
    {
        try {
            viewModel.StopWork();
        }
        catch (Exception)
        {
        }
        var saveWindow = new SaveAlgoDialog{viewModel = viewModel};
        saveWindow.ShowDialog();
    }
    private void OpenLoadWindow(object sender, RoutedEventArgs e)
    {
        var names = viewModel.GetPopulationsNames();
        var loadWindow = new LoadPopulation(names, viewModel);
        loadWindow.ShowDialog();
    }
    private void ResetArrangement(object sender, ExecutedRoutedEventArgs e)
    {
        viewModel.ResetAlgo();
    }
    private void CanReset(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = !viewModel.IsCurrentlyRunning;
    }
}
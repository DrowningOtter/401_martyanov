using System.Windows;

namespace OptimalSquaresUI
{
    public partial class LoadPopulation : Window
    {
        public ArrangementViewModel viewModel;
        public LoadPopulation(string[] Options, ArrangementViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            PopulationChoices.ItemsSource = Options;
        }
        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void OkClicked(object sender, RoutedEventArgs e)
        {
            var selected = PopulationChoices.SelectedItem.ToString();
            viewModel.LoadPopulation(selected);
            MessageBox.Show("population loaded successfully");
            this.DialogResult = true;
        }
    }
}
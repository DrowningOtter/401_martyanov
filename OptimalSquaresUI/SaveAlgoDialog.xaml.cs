using System.Windows;
using GenAlgo;

namespace OptimalSquaresUI
{
    public partial class SaveAlgoDialog : Window
    {
        public Arrangement? CurArrangement;
        public required ArrangementViewModel viewModel;
        public SaveAlgoDialog()
        {
            InitializeComponent();
        }
        private void OkClicked(object sender, RoutedEventArgs e)
        {
            if (viewModel.IsCurrentlyRunning)
            {
                MessageBox.Show("cannot save result, stop calculation first");
                return;
            }
            viewModel.SavePopulation(InputTextBox.Text);
            this.DialogResult = true;
            MessageBox.Show($"population {InputTextBox.Text} saved successfully");
        }
        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
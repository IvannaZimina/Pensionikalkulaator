using System.Windows;
using PensionCalculator.Core;

namespace PensionCalculator.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            TxtError.Text = string.Empty;
            TxtResult.Text = "—";

            try
            {
                string result = PensionLogic.Calculate(
                    TxtCurrentAge.Text,
                    TxtTargetAge.Text,
                    TxtCurrentSavings.Text,
                    TxtMonthlyContribution.Text,
                    TxtAnnualRate.Text
                );

                TxtResult.Text = result;
            }
            catch (System.Exception ex)
            {
                TxtError.Text = ex.Message;
            }
        }
    }
}
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



namespace ContractMonthlyClaimsSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenClaimWindow_Click(object sender, RoutedEventArgs e)
        {
            // Open Lecturer Claim Submission window
            var claimWindow = new ClaimSubmissionWindow();
            claimWindow.Show();
        }

      

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void OpenReviewWindow_Clicks(object sender, RoutedEventArgs e)
        {
            // Open Coordinator Review window
            var reviewWindow = new ClaimReviewWindow();
            reviewWindow.Show();
        }
    }
}

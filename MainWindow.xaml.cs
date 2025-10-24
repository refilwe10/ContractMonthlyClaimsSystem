using System.Windows;

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

        private void OpenReviewWindow_Click(object sender, RoutedEventArgs e)
        {
            // Open Coordinator Review window
            var reviewWindow = new ClaimReviewWindow();
            reviewWindow.Show();
        }

        private void OpenStatusWindow_Click(object sender, RoutedEventArgs e)
        {
            // Open Lecturer Claim Status tracking window
            var statusWindow = new ClaimStatusWindow();
            statusWindow.Show();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

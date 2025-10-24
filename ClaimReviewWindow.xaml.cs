using System.Linq;
using System.Windows;
using ContractMonthlyClaimsSystem.Data;
using ContractMonthlyClaimsSystem.Models;

namespace ContractMonthlyClaimsSystem
{
    public partial class ClaimReviewWindow : Window
    {
        public ClaimReviewWindow()
        {
            InitializeComponent();
            LoadPendingClaims();
        }

        private void LoadPendingClaims()
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            var pendingClaims = db.Claims
                .Where(c => c.Status == ClaimStatus.Pending)
                .OrderBy(c => c.DateSubmitted)
                .ToList();

            ClaimsDataGrid.ItemsSource = pendingClaims;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsDataGrid.SelectedItem is Claim selectedClaim)
            {
                using var db = new ApplicationDbContext();
                var claim = db.Claims.FirstOrDefault(c => c.ClaimId == selectedClaim.ClaimId);

                if (claim != null)
                {
                    claim.Status = ClaimStatus.Approved;
                    db.SaveChanges();
                    MessageBox.Show("✅ Claim approved successfully!");
                    LoadPendingClaims();
                }
            }
            else
            {
                MessageBox.Show("Please select a claim to approve.");
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsDataGrid.SelectedItem is Claim selectedClaim)
            {
                using var db = new ApplicationDbContext();
                var claim = db.Claims.FirstOrDefault(c => c.ClaimId == selectedClaim.ClaimId);

                if (claim != null)
                {
                    claim.Status = ClaimStatus.Rejected;
                    db.SaveChanges();
                    MessageBox.Show("❌ Claim rejected.");
                    LoadPendingClaims();
                }
            }
            else
            {
                MessageBox.Show("Please select a claim to reject.");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadPendingClaims();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


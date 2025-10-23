using ContractMonthlyClaimsSystem.Data;
using ContractMonthlyClaimsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContractMonthlyClaimsSystem
{
    /// <summary>
    /// Interaction logic for ClaimReviewWindow.xaml
    /// </summary>
    public partial class ClaimReviewWindow : Window
    {
        public ClaimReviewWindow()
        {
            InitializeComponent();
            LoadClaims();
        }

        private void LoadClaims()
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
            var pending = db.Claims
                            .OrderByDescending(c => c.DateSubmitted)
                            .ToList();
            ClaimsGrid.ItemsSource = pending;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                if (ClaimsGrid.SelectedItem is Claim claim)
                {
                    using var db = new ApplicationDbContext();
                    var record = db.Claims.Find(claim.ClaimId);
                    if (record != null)
                    {
                        record.Status = ClaimStatus.Approved;
                        db.SaveChanges();
                    }
                    MessageBox.Show($"Claim #{claim.ClaimId} approved.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClaims();
                }
                else
                {
                    MessageBox.Show("Please select a claim first.", "Info",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving claim: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ClaimsGrid.SelectedItem is Claim claim)
                {
                    string reason = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter reason for rejection:", "Reject Claim", "");

                    if (string.IsNullOrWhiteSpace(reason))
                        return;

                    using var db = new ApplicationDbContext();
                    var record = db.Claims.Find(claim.ClaimId);
                    if (record != null)
                    {
                        record.Status = ClaimStatus.Rejected;
                        record.Notes += $"\n[Rejected Reason: {reason}]";
                        db.SaveChanges();
                    }

                    MessageBox.Show($"Claim #{claim.ClaimId} rejected.", "Rejected",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClaims();
                }
                else
                {
                    MessageBox.Show("Please select a claim first.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting claim: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


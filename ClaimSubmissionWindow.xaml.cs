using System;
using System.Windows;
using ContractMonthlyClaimsSystem.Models;
using ContractMonthlyClaimsSystem.Data;

namespace ContractMonthlyClaimsSystem
{
    public partial class ClaimSubmissionWindow : Window
    {
        public ClaimSubmissionWindow()
        {
            InitializeComponent();
        }

        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new ApplicationDbContext();
                db.Database.EnsureCreated();

                // Get input from text boxes
                decimal hoursWorked = decimal.Parse(HoursWorkedTextBox.Text);
                decimal hourlyRate = decimal.Parse(HourlyRateTextBox.Text);

                // ✅ Formula: Total = Hours × Rate
                decimal totalAmount = hoursWorked * hourlyRate;

                // Create claim
                var claim = new Claim
                {
                    LecturerId = Environment.UserName,
                    HoursWorked = hoursWorked,
                    HourlyRate = hourlyRate,
                    TotalAmount = totalAmount,
                    Notes = NotesTextBox.Text,
                    DateSubmitted = DateTime.Now,
                    Status = ClaimStatus.Pending
                };

                // Save
                db.Claims.Add(claim);
                db.SaveChanges();

                MessageBox.Show($"Claim submitted successfully!\n\nTotal: R {totalAmount:F2}",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting claim: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

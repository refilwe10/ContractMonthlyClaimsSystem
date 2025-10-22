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
using ContractMonthlyClaimsSystem.Models;
using ContractMonthlyClaimsSystem.Data;

namespace ContractMonthlyClaimsSystem
{
    /// <summary>
    /// Interaction logic for ClaimStatusWindow.xaml
    /// </summary>
    public partial class ClaimStatusWindow : Window
    {
        public ClaimStatusWindow()
        {
            InitializeComponent();

            // whenever hours or rate changes, Recalculate total
            HoursWorkedBox.TextChanged += RecalculateTotal;
            HourlyRateBox.TextChanged += RecalculateTotal;
        }

        private void RecalculateTotal(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(HoursWorkedBox.Text, out decimal hours) &&
                decimal.TryParse(HourlyRateBox.Text, out decimal rate))
            {
                TotalBox.Text = (hours * rate).ToString("0.00");
            }
            else
            {
                TotalBox.Text = string.Empty;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate user input
                if (!decimal.TryParse(HoursWorkedBox.Text, out decimal hours) || hours <= 0)
                {
                    MessageBox.Show("Please enter a valid number of hours.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(HourlyRateBox.Text, out decimal rate) || rate <= 0)
                {
                    MessageBox.Show("Please enter a valid hourly rate.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create claim
                var claim = new Claim
                {
                    LecturerId = Environment.UserName, // you can replace this with a login ID later
                    HoursWorked = hours,
                    HourlyRate = rate,
                    TotalAmount = hours * rate,
                    Notes = NotesBox.Text,
                    Status = ClaimStatus.Pending,
                    DateSubmitted = DateTime.Now
                };

                // Save to DB
                using ApplicationDbContext db = new ApplicationDbContext();
                db.Database.EnsureCreated(); // make sure DB file exists
                db.Claims.Add(claim);
                db.SaveChanges();

                MessageBox.Show("Claim submitted successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting claim:\n{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenClaimWindow_Click(object sender, RoutedEventArgs e)
        {
            var win = new ContractMonthlyClaimsSystem.ClaimSubmissionWindow();
            win.ShowDialog();
        }
    }
}


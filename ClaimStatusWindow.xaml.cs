using System;
using System.Linq;
using System.Windows;
using ContractMonthlyClaimsSystem.Data;

namespace ContractMonthlyClaimsSystem
{
    public partial class ClaimStatusWindow : Window
    {
        public ClaimStatusWindow()
        {
            InitializeComponent();
            LoadLecturerClaims();
        }

        private void LoadLecturerClaims()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Database.EnsureCreated();

                    // Identify the current lecturer (Windows user)
                    var lecturerId = Environment.UserName;

                    // Retrieve all claims submitted by this lecturer
                    var claims = db.Claims
                                   .Where(c => c.LecturerId == lecturerId)
                                   .OrderByDescending(c => c.DateSubmitted)
                                   .ToList();

                    // Display results in DataGrid
                    LecturerClaimsGrid.ItemsSource = claims;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading claims:\n{ex.Message}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


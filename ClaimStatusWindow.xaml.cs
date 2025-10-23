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
            LoadLecturerClaims();
        }

        private void LoadLecturerClaims()
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            
            var lecturerId = System.Environment.UserName;

            var claims = db.Claims
                .Where(c => c.LecturerId == lecturerId)
                .OrderByDescending(c => c.DateSubmitted)
                .ToList();

            LecturerClaimsGrid.ItemsSource = claims;
        }
    }
}


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

    public partial class LecturesDashboard : Window
    {
        public LecturesDashboard()
        {
            InitializeComponent();

            // Fake data for demonstration purposes
            var claims = new List<dynamic>
            {
                new { ClaimID = 1, Date = "2025-09-01", HoursWorked = 20, Status = "Pending" },
                new { ClaimID = 2, Date = "2025-09-02", HoursWorked = 15, Status = "Approved" }
            };

            ClaimsDateGrid.ItemsSource = claims;
        }
    }
}

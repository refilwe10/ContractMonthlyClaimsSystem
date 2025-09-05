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

    public partial class LoginWindow : Window
    {
        // Remove the incorrect property declaration
        // public object RoleComboBox { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedRole = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedRole == "Lecturer")
            {
                LecturesDashboard lecturerDashboard = new LecturesDashboard();
                lecturerDashboard.Show();
                this.Close();
            }
            else if (selectedRole == "Coordinator" || selectedRole == "Manager")
            {
                CoordinatorDashboard coordinatorDashboard = new CoordinatorDashboard();
                coordinatorDashboard.Show();
                this.Close();
            }
        }
    }
}
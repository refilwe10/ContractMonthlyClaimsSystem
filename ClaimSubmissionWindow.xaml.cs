using ContractMonthlyClaimsSystem.Data;
using ContractMonthlyClaimsSystem.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace ContractMonthlyClaimsSystem
{
    public partial class ClaimSubmissionWindow : Window
    {
        private string uploadedFilePath = string.Empty;

        public ClaimSubmissionWindow()
        {
            InitializeComponent();
            HoursWorkedBox.TextChanged += RecalculateTotal;
            HourlyRateBox.TextChanged += RecalculateTotal;
        }

        private void RecalculateTotal(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(HoursWorkedBox.Text, out decimal hours) && !string.IsNullOrEmpty(HourlyRateBox.Text) )
                HoursWorkedBox.BorderBrush = System.Windows.Media.Brushes.Red;
            
               
            else
            HoursWorkedBox.ClearValue(System.Windows.Controls.Border.BorderBrushProperty);

            if (!decimal.TryParse(HourlyRateBox.Text, out _) && !string.IsNullOrEmpty(HoursWorkedBox.Text))
                HourlyRateBox.BorderBrush = System.Windows.Media.Brushes.Red;
            else
                HourlyRateBox.ClearValue(System.Windows.Controls.Border.BorderBrushProperty);
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select Supporting Document",
                Filter = "PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string uploadsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string fileName = Path.GetFileName(openFileDialog.FileName);
                    string destinationPath = Path.Combine(uploadsFolder, fileName);

                    File.Copy(openFileDialog.FileName, destinationPath, true);

                    uploadedFilePath = destinationPath;
                    FileNameText.Text = fileName;
                    FileNameText.Foreground = Brushes.Black;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading file:\n{ex.Message}", "Upload Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

                using (var db = new ApplicationDbContext())
                {
                    db.Database.EnsureCreated();

                    var claim = new Claim
                    {
                        LecturerId = Environment.UserName,
                        HoursWorked = hours,
                        HourlyRate = rate,
                        TotalAmount = hours * rate,
                        Notes = NotesBox.Text,
                        SupportingDocumentPath = uploadedFilePath,
                        Status = ClaimStatus.Pending,
                        DateSubmitted = DateTime.Now
                    };

                    db.Claims.Add(claim);
                    db.SaveChanges();
                }

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
    }
}

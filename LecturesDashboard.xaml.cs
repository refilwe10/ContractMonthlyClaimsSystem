using ContractMonthlyClaimsSystem.Data;
using ContractMonthlyClaimsSystem.Models;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;

namespace ContractMonthlyClaimsSystem
{
    public partial class LecturesDashboard : Window
    {
        public LecturesDashboard()
        {
            InitializeComponent();
            LoadClaims();
        }

        private void LoadClaims()
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            
            var lecturerId = System.Environment.UserName;

            var claims = db.Claims
                .Where(c => c.LecturerId == lecturerId)
                .OrderByDescending(c => c.DateSubmitted)
                .ToList();

            ClaimsDataGrid.ItemsSource = claims;
        }

        private void OpenClaimWindow_Click(object sender, RoutedEventArgs e)
        {
            var claimWindow = new ClaimSubmissionWindow();
            claimWindow.ShowDialog();
            LoadClaims(); // 
        }

     
            private void UploadDocument_Click(object sender, RoutedEventArgs e)
            {
                // Make sure a claim is selected first
                if (ClaimsDataGrid.SelectedItem is not Claim selectedClaim)
                {
                    MessageBox.Show("Please select a claim first before uploading a document.", "No Claim Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Let user pick a file
                var dialog = new OpenFileDialog
                {
                    Title = "Select Supporting Document",
                    Filter = "Documents (*.pdf;*.docx;*.jpg;*.png)|*.pdf;*.docx;*.jpg;*.png"
                };

                if (dialog.ShowDialog() == true)
                {
                    string sourcePath = dialog.FileName;
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                    //  Create uploads folder if missing
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    //  Save the uploaded file
                    string fileName = $"{selectedClaim.ClaimId}_{Path.GetFileName(sourcePath)}";
                    string destinationPath = Path.Combine(uploadsFolder, fileName);
                    File.Copy(sourcePath, destinationPath, overwrite: true);

                    //  Update claim record
                    using var db = new ApplicationDbContext();
                    var claimToUpdate = db.Claims.FirstOrDefault(c => c.ClaimId == selectedClaim.ClaimId);
                    if (claimToUpdate != null)
                    {
                        claimToUpdate.SupportingDocumentPath = destinationPath;
                        db.SaveChanges();
                    }

                    MessageBox.Show("Document uploaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClaims();
                }
            }

            private void Logout_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }
    }


   
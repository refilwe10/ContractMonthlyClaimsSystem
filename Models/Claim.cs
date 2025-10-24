using System;

namespace ContractMonthlyClaimsSystem.Models
{
    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected,
        Paid
    }

    public class Claim
    {
        public int ClaimId { get; set; }

        // Lecturer details
        public string LecturerId { get; set; } = string.Empty;
        public string? LecturerName { get; set; }

        // Claim details
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }

        // Supporting info
        public string? Notes { get; set; }

        // File attachment (only keep one consistent property)
        public string SupportingDocumentPath { get; set; } = string.Empty;


        // Claim status
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        // Date submitted
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }

}

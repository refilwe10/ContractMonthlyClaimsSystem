using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string LecturerId { get; set; } = string.Empty;
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public string? AttachmentPath { get; set; }
        public string SupportingDocumentPath { get; set; } = string.Empty;
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;


        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public string SupportingDocumentName { get; set; }
        public string FilePath { get; set; }


    }
        }
    






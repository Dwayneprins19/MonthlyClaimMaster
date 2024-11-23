using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonthlyClaimMaster.Data;
using MonthlyClaimMaster.Models;

namespace MonthlyClaimMaster.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public DateTime DateSubmitted { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalPayment { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public IFormFile SupportingDocument { get; set; }
    }
}

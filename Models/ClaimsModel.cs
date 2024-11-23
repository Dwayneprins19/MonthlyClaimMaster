using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonthlyClaimMaster.Data;
using MonthlyClaimMaster.Models;

namespace MonthlyClaimMaster.Models
{
    public class ClaimsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public MonthlyClaimMaster.Models.Claim Claim { get; set; }

        public ClaimsModel(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            Claim = new MonthlyClaimMaster.Models.Claim();
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            // Handle file upload
            if (Claim.SupportingDocument != null)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, Claim.SupportingDocument.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Claim.SupportingDocument.CopyToAsync(fileStream);
                }
            }
            Claim.DateSubmitted = DateTime.Now;
            Claim.TotalPayment = Claim.HoursWorked * Claim.HourlyRate;
            Claim.Status = "Pending"; _context.Claims.Add(Claim);
            await _context.SaveChangesAsync(); 
            
            return RedirectToPage("/Claims/Track");
        }
    }
}

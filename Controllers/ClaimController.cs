using Microsoft.AspNetCore.Mvc;
using MonthlyClaimMaster.Data;
using MonthlyClaimMaster.Models;

namespace MonthlyClaimMaster.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Claim claim)
        {
            if(ModelState.IsValid)
            {
                claim.DateSubmitted = DateTime.Now;
                claim.TotalPayment = claim.HoursWorked * claim.HourlyRate;
                claim.Status = "Pending";
                _context.Claims.Add(claim);
                _context.SaveChanges();
                return RedirectToAction("Track");
            }
            return View(claim);
        }

        public IActionResult Track()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        public IActionResult Verify()
        {
            var claims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View(claims);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                _context.SaveChanges();
            }
            return RedirectToAction("Verify");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("Verify");
        }

    }
}

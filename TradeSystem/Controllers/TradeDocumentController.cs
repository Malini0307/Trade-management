using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.NetworkInformation;
using TradeSystem.Data;
using TradeSystem.Interfaces;
using TradeSystem.Models;

namespace TradeSystem.Controllers
{
    [Authorize]
    public class TradeDocumentController : Controller
    {
        private readonly ITradeDocumentService _service;
        private readonly TfmsDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TradeDocumentController(
            ITradeDocumentService service,
            TfmsDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _db = db;
            _userManager = userManager;
        }

        // Common doc types dropdown
        private static readonly string[] _docTypes = new[]
        {
            "Commercial Invoice", "Packing List", "Bill of Lading", "Air Waybill",
            "Insurance Certificate", "Certificate of Origin", "Shipping Bill"
        };

        // Unique reference like ABC09876
        private string GenerateUniqueReference()
        {
            var rnd = new Random();
            string candidate;
            do
            {
                candidate = "ABC" + rnd.Next(10000, 99999);
            }
            while (_db.TradeDocuments.Any(d => d.ReferenceNumber == candidate));
            return candidate;
        }

        private async Task<string> GetCurrentDisplayNameAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && !string.IsNullOrWhiteSpace(user.FullName))
                return user.FullName;
            return User.Identity?.Name ?? "Unknown";
        }

        private void LoadLookups()
        {
            // LC visible if status is Open or Amended
            var lcs = _db.LetterOfCredits
                         .Where(l => l.Status == LCStatus.Open || l.Status == LCStatus.Amended)
                         .Select(l => new { l.LcId, Label = $"LC #{l.LcId} - {l.BeneficiaryName}" })
                         .ToList();

            // BG visible if Issued
            var bgs = _db.BankGuarantees
                         .Where(g => g.Status == BgStatus.Issued)
                         .Select(g => new { g.GuaranteeId, Label = $"BG #{g.GuaranteeId} - {g.BeneficiaryName}" })
                         .ToList();

            ViewBag.LCs = new SelectList(lcs, "LcId", "Label");
            ViewBag.BGs = new SelectList(bgs, "GuaranteeId", "Label");
            ViewBag.DocTypes = new SelectList(_docTypes);
        }

        // List
        public IActionResult Index()
        {
            var docs = _service.GetAllDocumentsById();
            return View(docs);
        }

        // Upload (User only)
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Upload()
        {
            var model = new TradeDocument
            {
                ReferenceNumber = GenerateUniqueReference(),
                Status = TdStatus.Active
            };
            LoadLookups();
            return View(model);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(TradeDocument doc)
        {
            if (!ModelState.IsValid)
            {
                LoadLookups();
                return View(doc);
            }

            try
            {
                // Force server-side values
                doc.ReferenceNumber = string.IsNullOrWhiteSpace(doc.ReferenceNumber)
                                      ? GenerateUniqueReference()
                                      : doc.ReferenceNumber;

                doc.UploadedBy = await GetCurrentDisplayNameAsync();

                if (!_service.UploadDocument(doc))
                {
                    ModelState.AddModelError("", "Failed to upload document (duplicate reference or server error).");
                    LoadLookups();
                    return View(doc);
                }

                return Content("<script>window.location.href='/TradeDocument/Index';</script>","text/html");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",$"Upload Failed: {ex.Message}");
                LoadLookups();
                return View(doc);
            }
            
        }

        // Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var doc = _service.ViewDocument(id);
            if (doc == null) return NotFound();
            LoadLookups();
            return View(doc);
        }

        // Edit (User only)
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var doc = _service.ViewDocument(id);
            if (doc == null) return NotFound();
            LoadLookups();
            return View(doc);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(TradeDocument doc)
        {
            if (!ModelState.IsValid)
            {
                LoadLookups();
                return View(doc);
            }

            if (!_service.UpdateDocumentDetails(doc))
            {
                ModelState.AddModelError("", "Failed to update document.");
                LoadLookups();
                return View(doc);
            }

            return RedirectToAction(nameof(Index));
        }


    }

}

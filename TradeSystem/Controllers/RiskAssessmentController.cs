using Microsoft.AspNetCore.Mvc;
using TradeSystem.Data;
using TradeSystem.Interfaces;
using TradeSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TradeSystem.Controllers
{
    [Route("[controller]")]
    public class RiskAssessmentController : Controller
    {
        private readonly TfmsDbContext context;
        private readonly IRiskAssessmentService _riskService;

        public RiskAssessmentController(IRiskAssessmentService riskService, TfmsDbContext context)
        {
            _riskService = riskService;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var lcItems = context.LetterOfCredits
                                  .Where(l => l.Status == LCStatus.Open || l.Status == LCStatus.Amended)
                                  .OrderByDescending(l => l.LcId)
                                  .Select(l => new SelectListItem
                                  {
                                      Value = l.LcId.ToString(),
                                      Text = $"LC #{l.LcId} — {l.ApplicantName} → {l.BeneficiaryName} ({l.Currency} {l.Amount})"
                                  })
                                  .ToList();
            ViewBag.LCs = new SelectList(lcItems, "Value", "Text");
            return View();
        }

        [HttpPost("analyze/lc/{lcId}")]
        public ActionResult<RiskAssessment> AnalyzeByLc(int lcId)
        {
            try { return Ok(_riskService.AnalyzeByLcId(lcId)); }
            catch (ArgumentException ex) { return NotFound(ex.Message); }
        }

        [HttpPost("analyze/bg/{guaranteeId}")]
        public ActionResult<RiskAssessment> AnalyzeByBg(int guaranteeId)
        {
            try { return Ok(_riskService.AnalyzeByBgId(guaranteeId)); }
            catch (ArgumentException ex) { return NotFound(ex.Message); }
        }

        [HttpPost("analyze/ref")]
        public ActionResult<RiskAssessment> AnalyzeByReference([FromBody] string referenceNumber)
        {
            try { return Ok(_riskService.AnalyzeByReference(referenceNumber)); }
            catch (ArgumentException ex) { return NotFound(ex.Message); }
        }

        [HttpPost("analyze/collective/{lcId}")]
        public ActionResult<RiskAssessment> AnalyzeCollective(int lcId)
        {
            try { return Ok(_riskService.AnalyzeCollectiveByLcId(lcId)); }
            catch (ArgumentException ex) { return NotFound(ex.Message); }
        }

        [HttpGet("score/{riskId}")]
        public ActionResult<decimal> GetRiskScore(int riskId)
        {
            try { return Ok(_riskService.GetRiskScore(riskId)); }
            catch (ArgumentException ex) { return NotFound(ex.Message); }
        }
    }
}

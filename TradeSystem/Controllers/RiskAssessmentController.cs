using Microsoft.AspNetCore.Mvc;
using TradeSystem.Data;
using TradeSystem.Interfaces;
using TradeSystem.Models;

namespace TradeSystem.Controllers
{
    public class RiskAssessmentController : Controller
    {
        private readonly TfmsDbContext context;
        private readonly IRiskAssessmentService _riskService;

        public RiskAssessmentController(IRiskAssessmentService riskService, TfmsDbContext context)
        {
            _riskService = riskService;
        }

        [HttpPost("analyze")]
        public ActionResult<RiskAssessment> AnalyzeRisk([FromBody] string transactionReference)
        {
            if (string.IsNullOrWhiteSpace(transactionReference))
                return BadRequest("Transaction reference is required.");

            try
            {
                var assessment = _riskService.AnalyzeRisk(transactionReference);
                return Ok(assessment);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/RiskAssessment/score/5
        [HttpGet("score/{riskId}")]
        public ActionResult<decimal> GetRiskScore(int riskId)
        {
            try
            {
                var score = _riskService.GetRiskScore(riskId);
                return Ok(score);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

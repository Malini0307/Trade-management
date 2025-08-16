using Microsoft.AspNetCore.Mvc;
using TradeSystem.Interfaces;
using TradeSystem.Models;

namespace TradeSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplianceController : ControllerBase
    {
        private readonly IComplianceService _complianceService;

        public ComplianceController(IComplianceService complianceService)
        {
            _complianceService = complianceService;
        }

        // POST: api/Compliance/generate
        [HttpPost("generate")]
        public IActionResult GenerateReport([FromBody] Compliance compliance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _complianceService.GenerateComplianceReport(compliance);
            return success ? Ok("Compliance report generated.") : StatusCode(500, "Failed to generate report.");
        }

        // PUT: api/Compliance/submit
        [HttpPut("submit")]
        public IActionResult SubmitReport([FromBody] Compliance compliance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _complianceService.SubmitComplianceReport(compliance);
            return success ? Ok("Compliance report submitted.") : NotFound("Compliance record not found.");
        }

        // GET: api/Compliance/5
        [HttpGet("{id}")]
        public ActionResult<Compliance> GetById(int id)
        {
            var compliance = _complianceService.GetComplianceById(id);
            return compliance != null ? Ok(compliance) : NotFound("Compliance not found.");
        }

        // GET: api/Compliance
        [HttpGet]
        public ActionResult<List<Compliance>> GetAll()
        {
            var compliances = _complianceService.GetAllCompliances();
            return Ok(compliances);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TradeSystem.Data;
using TradeSystem.Interfaces;
using TradeSystem.Models;

namespace TradeSystem.Services
{
    public class ComplianceService : IComplianceService
    {
        private readonly TfmsDbContext _context;

        public ComplianceService(TfmsDbContext context)
        {
            _context = context;
        }

        public bool GenerateComplianceReport(Compliance compliance)
        {
            if (compliance == null || string.IsNullOrWhiteSpace(compliance.TransactionReference))
                return false;

            compliance.ReportDate = DateTime.UtcNow;
            _context.Compliances.Add(compliance);
            return _context.SaveChanges() > 0;
        }

        public bool SubmitComplianceReport(Compliance compliance)
        {
            var existing = _context.Compliances.Find(compliance.ComplianceId);
            if (existing == null) return false;

            existing.ComplianceStatus = compliance.ComplianceStatus;
            existing.Remarks = compliance.Remarks;
            existing.ReportDate = DateTime.UtcNow;

            return _context.SaveChanges() > 0;
        }

        public Compliance GetComplianceById(int complianceId)
        {
            return _context.Compliances
                .Include(c => c.LetterOfCredit)
                .Include(c => c.BankGuarantee)
                .FirstOrDefault(c => c.ComplianceId == complianceId);
        }

        public List<Compliance> GetAllCompliances()
        {
            return _context.Compliances
                .Include(c => c.LetterOfCredit)
                .Include(c => c.BankGuarantee)
                .ToList();
        }
    }
}


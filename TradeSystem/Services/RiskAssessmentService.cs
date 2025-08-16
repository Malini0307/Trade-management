using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TradeSystem.Data;
using TradeSystem.Interfaces;
using TradeSystem.Models;

namespace TradeSystem.Services
{
    public class RiskAssessmentService : IRiskAssessmentService
    {
        private readonly TfmsDbContext _context;
        public RiskAssessmentService(TfmsDbContext context)
        {
            this._context = context;
        }

        public RiskAssessment AnalyzeRisk(string transactionReference)
        {
            var lc = _context.LetterOfCredits
                .FirstOrDefault(l => l.ApplicantName == transactionReference || l.BeneficiaryName == transactionReference);

            var bg = _context.BankGuarantees
                .FirstOrDefault(b => b.ApplicantName == transactionReference || b.BeneficiaryName == transactionReference);

            if (lc == null && bg == null)
                throw new ArgumentException("No LC or Bank Guarantee found for the given transaction reference.");

            var factors = new Dictionary<string, object>();
            decimal rawScore = 0;

            // LC Factors
            if (lc != null)
            {
                decimal lcAmtWeight = lc.Amount > 1000000 ? 30 : 10;
                rawScore += lcAmtWeight;
                factors["LCAmount"] = lc.Amount;

                var lcDaysLeft = (lc.ExpiryDate - DateTime.Now).TotalDays;
                decimal lcExpiryWeight = lcDaysLeft < 30 ? 25 : 5;
                rawScore += lcExpiryWeight;
                factors["LCExpiry"] = lc.ExpiryDate;

                decimal lcStatusWeight = lc.Status switch
                {
                    LCStatus.Amended => 15,
                    LCStatus.Closed => 20,
                    _ => 5
                };
                rawScore += lcStatusWeight;
                factors["LCStatus"] = lc.Status.ToString();
            }

            // BG Factors
            if (bg != null)
            {
                decimal bgAmtWeight = bg.GuaranteeAmount > 1000000 ? 30 : 10;
                rawScore += bgAmtWeight;
                factors["BGAmount"] = bg.GuaranteeAmount;

                var bgDaysLeft = (bg.ValidityPeriod - DateTime.Now).TotalDays;
                decimal bgValidityWeight = bgDaysLeft < 30 ? 25 : 5;
                rawScore += bgValidityWeight;
                factors["BGValidity"] = bg.ValidityPeriod;

                decimal bgStatusWeight = bg.Status switch
                {
                    BgStatus.Pending => 20,
                    BgStatus.Expired => 40,
                    _ => 5
                };
                rawScore += bgStatusWeight;
                factors["BGStatus"] = bg.Status.ToString();
            }

            // Normalize score
            decimal normalizedScore = Math.Min((rawScore / 165m) * 100, 100);

            var assessment = new RiskAssessment
            {
                TransactionReference = transactionReference,
                RiskFactors = JsonSerializer.Serialize(factors),
                RiskScore = normalizedScore,
                AssessmentDate = DateTime.Now,
                LcId = lc?.LcId,
                GuaranteeId = bg?.GuaranteeId
            };

            _context.RiskAssessments.Add(assessment);
            _context.SaveChanges();

            return assessment;
        }

        public decimal GetRiskScore(int riskId)
        {
            var assessment = _context.RiskAssessments.Find(riskId);
            if (assessment == null)
                throw new ArgumentException("Risk assessment not found.");

            return assessment.RiskScore;
        }



    }
}





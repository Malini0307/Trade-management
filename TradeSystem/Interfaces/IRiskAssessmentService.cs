using TradeSystem.Models;

namespace TradeSystem.Interfaces
{
    public interface IRiskAssessmentService
    {
        RiskAssessment AnalyzeRisk(string transactionReference);
        decimal GetRiskScore(int riskId);
    }
}

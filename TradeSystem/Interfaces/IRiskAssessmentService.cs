using TradeSystem.Models;

namespace TradeSystem.Interfaces
{
    public interface IRiskAssessmentService
    {
        RiskAssessment AnalyzeByLcId(int lcId);
        RiskAssessment AnalyzeByBgId(int guaranteeId);
        RiskAssessment AnalyzeByReference(string referenceNumber);
        decimal GetRiskScore(int riskId);
    }
}

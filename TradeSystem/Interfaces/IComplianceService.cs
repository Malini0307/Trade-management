using TradeSystem.Models;

namespace TradeSystem.Interfaces
{
    public interface IComplianceService
    {
        bool GenerateComplianceReport(Compliance compliance);
        bool SubmitComplianceReport(Compliance compliance);
        Compliance GetComplianceById(int complianceId);
        List<Compliance> GetAllCompliances();

    }
}

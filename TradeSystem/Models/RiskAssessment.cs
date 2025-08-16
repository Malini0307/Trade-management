using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSystem.Models
{
    [Table("RiskAssessments")]
    public class RiskAssessment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RiskId { get; set; }

        [Required(ErrorMessage = "The Transaction Reference is required"), MaxLength(10)]
        public string TransactionReference { get; set; }

        [Required(ErrorMessage = "The Risk Factors are required"), MaxLength(10)]
        public string RiskFactors { get; set; } // Stored as JSON-like string or XML for flexibility
        [Required]
        public decimal RiskScore { get; set; }

        [Required(ErrorMessage = "The Date is required"), MaxLength(10)]
        public DateTime AssessmentDate { get; set; }

        public int? LcId { get; set; }
        [ForeignKey("LcId")]
        public LetterOfCredit LetterOfCredit { get; set; }

        public int? GuaranteeId { get; set; }
        [ForeignKey("GuaranteeId")]
        public BankGuarantee BankGuarantee { get; set; }

    }
}

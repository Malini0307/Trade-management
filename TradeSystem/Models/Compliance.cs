using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSystem.Models
{
    public enum Status4
    {
        [Display(Name = "Compliant")]
        Compliant,
        [Display(Name = "Non-Compliant")]
        Non_Compliant
    }
    [Table("Compliances")]
    public class Compliance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplianceId { get; set; }

        [Required(ErrorMessage = "The Transaction Reference is required"), MaxLength(20)]
        public string TransactionReference { get; set; }

        [Required(ErrorMessage = "The Compliance Status is required"), MaxLength(20)]
        public Status4 ComplianceStatus { get; set; } 

        [Required(ErrorMessage = "The Remarks is required"), MaxLength(20)]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "The Date is required"), MaxLength(20)]
        public DateTime ReportDate { get; set; }

        public int? LcId { get; set; }
        [ForeignKey("LcId")]
        public LetterOfCredit LetterOfCredit { get; set; }

        public int? GuaranteeId { get; set; }
        [ForeignKey("GuaranteeId")]
        public BankGuarantee BankGuarantee { get; set; }
    }
}

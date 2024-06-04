using System.ComponentModel.DataAnnotations;

namespace SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel.RequestModel
{
   
    public class ProtectShieldModel
    {
        [DataType(DataType.Date)]
        [Required]
        //[RegularExpression("yyyy-MM-dd")]
        public string? InwardDate { get; set; }

        [Required]
        public string? ApplicationNo { get; set; }
        ///<example>BOI</example>
        [Required]

        public string? Source { get; set; }

        [Required]
        public string CustomerID { get; set; }
        public AddFieldPS AddField { get; set; }
        public ApplicantDetailsPS ApplicantDetails { get; set; }
        public ProposerDetailsPS ProposerDetails { get; set; }
        [Required]
        public string PremiumPaymentTerm { get; set; }
        [Required]
        public int PolicyTerm { get; set; }

        /// <summary> 
        ///  Mapping Values for PremiumPaymentModes:
        /// 1.  "Annual"
        /// 2.  "Semi-Annual"
        /// 3.  "Quarterly (ecs/ si)"
        /// 4.  "Monthly (ecs/ si)"
        /// 5.  "Single"
        /// </summary>
        [Required]
        public string PremiumPaymentModes { get; set; }
        /// <summary>
        /// Mapping Values for StandardAgeProof:
        ///1.	“Yes”
        ///2.	“No” 
        /// </summary>
        [Required]
        public string? StandardAgeProof { get; set; }
        /// <summary>
        ///  Mapping Values for Smoke:
        /// 1.  "Non - Smoker" 
        /// 2.  "Smoker"
        /// </summary>
        [Required]
        public string Smoke { get; set; }
        /// <summary>
        ///  Mapping Values for BenefitOption:
        /// 1.  "Life Cover" 
        /// 2.  "Life Cover With Return Of Premium"
        /// 3.  "Life cover with critical illness"
        ///  </summary>
        [Required]
        public string BenefitOption { get; set; }
        /// <summary>
        ///  Mapping Values for PayoutOption:
        /// 1.  "Lump Sum" 
        /// 2.  "Monthly Income"
        /// 3.  "Lump-sum Plus Monthly Income"
        ///  </summary>
        [Required]

        public string PayoutOption { get; set; }
        /// <summary>
        ///  Mapping Values for DistributionChannel:
        ///1.	“Corporate Agency”
        ///2.	“Direct Channel”
        ///3.	“Agency”
        ///4.	“Broker”
        ///5.	“Insurance Marketing Firm”
        ///6.	“Online”
        ///7.	“Web Aggregator”
        /// </summary>
        [Required]
        public string DistributionChannel { get; set; }
        [Required]
        public int SumAssured { get; set; }
        /// <summary>
        ///  Mapping Values for StaffPolicy:
        /// 1.  "Yes" 
        /// 2.  "No"
        /// 3.  "Sud Life Staff/Family"
        /// 4.  "Direct Sales Team"
        /// </summary>
        [Required]
        public string StaffPolicy { get; set; }
        [Required]
        public string ADTPDRiderOpted { get; set; }
        [Required]
        public int ADTPDRiderSA { get; set; }
    }

    public class AddFieldPS
    {
        public List<Dummy> Dummy { get; set; }
    }

    public class ApplicantDetailsPS
    {
        [Required]
        public string? ApplicantFName { get; set; }

        [Required]
        public string? ApplicantLName { get; set; }

        [Required]
        public string? ApplicantDateOfBirth { get; set; }

        [Required]
        public string? ApplicantGender { get; set; }

        [Required]
        public string? ApplicantEmail { get; set; }

        [Required]
        public string? ApplicantContactNumber { get; set; }

        public string ApplicantCity { get; set; }
        public string ApplicantState { get; set; }
    }

    

    public class ProposerDetailsPS
    {
        [Required]
        public string? IsProposersameasApplicant { get; set; }

        [Required]
        public string? ProposerFName { get; set; }

        [Required]
        public string? ProposerLName { get; set; }

        [Required]
        public string? ProposerDateOfBirth { get; set; }
        [Required]
        public string? ProposerGender { get; set; }
        [Required]
        public string? ProposerEmail { get; set; }
        [Required]
        public string ProposerContactNumber { get; set; }
        public string ProposerCity { get; set; }
        public string ProposerState { get; set; }
    }

}

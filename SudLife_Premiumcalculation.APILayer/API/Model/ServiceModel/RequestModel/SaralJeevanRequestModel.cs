using System.ComponentModel.DataAnnotations;

namespace SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel.RequestModel
{
    public class SaralJeevanRequestModel
    {

        [DataType(DataType.Date)]
        [Required]
        [RegularExpression("yyyy-MM-dd")]
        public string? InwardDate { get; set; }

        [Required]
        public string? ApplicationNo { get; set; }
        ///<example>BOI</example>
        [Required]

        public string? Source { get; set; }

        [Required]
        public string CustomerID { get; set; }
        public AddField AddField { get; set; }
        public ApplicantDetails ApplicantDetails { get; set; }
        public ProposerDetails ProposerDetails { get; set; }

        [Required]
        public int PremiumPaymentTerm { get; set; }
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
        [Required]
        public string KerelaFloodsCESSApplicable { get; set; }
        /// <summary>
        ///  Mapping Values for DistributionChannel:
        ///1.	“Corporate Agency”
        ///2.	“Direct Channel”
        ///3.	“Agency”
        ///4.	“Broker”
        ///5.	“Insurance Marketing Firm”
        ///5.	“Online”
        /// </summary>
        [Required]
        public string DistributionChannel { get; set; }
        /// <summary>
        ///  Mapping Values for StaffPolicy:
        /// 1.  "Corporate Agency Staff" 
        /// 2.  "Yes"
        /// 3.  "No"
        /// 4.  "Sud Life Staff/Family"
        /// 5.  "Direct Sales Team"
        /// 6.  "Online- Others"
        /// </summary>
        [Required]
        public string StaffPolicy { get; set; }
        [Required]
        public int AnnualPremium { get; set; }
        [Required]
        public int SumAssured { get; set; }
        /// <summary>
        /// Mapping Values for StandardAgeProof:
        ///1.	“Yes”
        ///2.	“No” 
        /// </summary>
        [Required]
        public string StandardAgeProof { get; set; }

    }

    public class Dummy
    {
        public string DummyName { get; set; }
        public string DummyValue { get; set; }
    }

    public class AddField
    {
        public List<Dummy> Dummy { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ApplicantDetails
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
        public string ApplicantContactNumber { get; set; }
        public string ApplicantCity { get; set; }
        public string ApplicantState { get; set; }
    }

    public class ProposerDetails
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

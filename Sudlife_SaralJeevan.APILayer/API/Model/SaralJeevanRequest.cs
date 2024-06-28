using System.ComponentModel.DataAnnotations;

namespace Sudlife_SaralJeevan.APILayer.API.Model
{
    public class SaralJeevanRequest
    {
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public string? InwardDate { get; set; }

        [Required]
        public string? ApplicationNo { get; set; }
     
        [Required]

        public string? Source { get; set; }

        [Required]
        public string CustomerID { get; set; }
        public AddField AddField { get; set; }
        public ApplicantDetails ApplicantDetails { get; set; }
        public ProposerDetails ProposerDetails { get; set; }
      
        [Required]
        public string PremiumPaymentTerm { get; set; }
        [Required]
        public int PolicyTerm { get; set; }
       
        [Required]
        public string PremiumPaymentModes { get; set; }
        [Required]
        public string KerelaFloodsCESSApplicable { get; set; }
       
        [Required]
        public string DistributionChannel { get; set; }
        
        [Required]
        public string StaffPolicy { get; set; }
        [Required]
        public int AnnualPremium { get; set; }
        [Required]
        public int SumAssured { get; set; }
       
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

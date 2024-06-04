namespace SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel
{
    public class TermPlanRequestModel
    {
        public DateTime InwardDate { get; set; }
        public string ApplicationNo { get; set; }
        public string Source { get; set; }
        public string CustomerID { get; set; }
        public AddField AddField { get; set; }
        public ApplicantDetails ApplicantDetails { get; set; }
        public ProposerDetails ProposerDetails { get; set; }
        public int PremiumPaymentTerm { get; set; }
        public int PolicyTerm { get; set; }
        public string PremiumPaymentModes { get; set; }
        public string KerelaFloodsCESSApplicable { get; set; }
        public string DistributionChannel { get; set; }
        public string StaffPolicy { get; set; }
        public decimal AnnualPremium { get; set; }
        public decimal SumAssured { get; set; }
        public string StandardAgeProof { get; set; }
        public string ADTPDRiderOpted { get; set; }
        public decimal ADTPDRiderSA { get; set; }
        public string Smoke { get; set; }
        public string BenefitOption { get; set; }
        public string PayoutOption { get; set; }
    }

    public class AddField
    {
        public List<Dummy> Dummy { get; set; }
    }
    public class Dummy
    {
        public string DummyName { get; set; }
        public string DummyValue { get; set; }
    }
    public class ApplicantDetails
    {
        public string ApplicantFName { get; set; }
        public string ApplicantLName { get; set; }
        public string ApplicantDateOfBirth { get; set; }
        public string ApplicantGender { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantContactNumber { get; set; }
        public string ApplicantCity { get; set; }
        public string ApplicantState { get; set; }
    }
    public class ProposerDetails
    {
        public string IsProposerSameAsApplicant { get; set; }
        public string ProposerFName { get; set; }
        public string ProposerLName { get; set; }
        public string ProposerDateOfBirth { get; set; }
        public string ProposerGender { get; set; }
        public string ProposerEmail { get; set; }
        public string ProposerContactNumber { get; set; }
        public string ProposerCity { get; set; }
        public string ProposerState { get; set; }
    }
  
}

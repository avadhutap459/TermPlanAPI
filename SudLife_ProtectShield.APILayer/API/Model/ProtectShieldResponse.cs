namespace SudLife_ProtectShield.APILayer.API.Model
{
    public class ProtectShieldResponse
    {
        public double ModalPremium { get; set; }

        public double Tax { get; set; }

        public double ModalPremiumwithTax { get; set; }

        public double AnnualPremium { get; set; }

        public double AATPDPremium { get; set; }

        public double AATPDTax { get; set; }

        public double AATPDwithTax { get; set; }

        public double AATPDAnnualPremium { get; set; }

        public double TotalPremium { get; set; }

        public double TotalTAX { get; set; }

        public double TotalPremiumwithTax { get; set; }

        public double TotalAnnualPremium { get; set; }

        public string Message { get; set; }

        public string Status { get; set; }

        public int TransactionId { get; set; }
    }
    
}

namespace Sudlife_SaralJeevan.APILayer.API.Model
{
    public class SaralJeevanResponse
    {
        public double ModalPremium { get; set; }

        public double Tax { get; set; }

        public double ModalPremiumwithTax { get; set; }

        public object Message { get; set; }

        public string Status { get; set; }

        public int TransactionId { get; set; }
    }
}

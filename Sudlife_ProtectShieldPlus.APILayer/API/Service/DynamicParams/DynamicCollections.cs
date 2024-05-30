namespace Sudlife_ProtectShieldPlus.APILayer.API.Service.DynamicParams
{
    public class DynamicCollections
    {
        public string PremiumPaymentTermProtectShieldPlus(string PremiumPaymentTerm, string PolicyTerm)
        {
            if (PremiumPaymentTerm == "regular pay")
            {
                return PremiumPaymentTerm = PolicyTerm;
            }
            else
            {
                Dictionary<string, string> PremiumPayment = new Dictionary<string, string>();
                PremiumPayment.Add("single pay", "1");
                PremiumPayment.Add("5 pay", "5");
                PremiumPayment.Add("7 pay", "7");
                PremiumPayment.Add("10 pay", "10");
                PremiumPayment.Add("12 pay", "12");
                PremiumPayment.Add("15 pay", "15");
                string Premium = PremiumPayment[PremiumPaymentTerm].ToString();
                return Premium;
            }
        }

        public string InputMode(string Mode)
        {
            Dictionary<string, string> DictInputMode = new Dictionary<string, string>();

            DictInputMode.Add("annual", "1");
            DictInputMode.Add("semi-annual", "2");
            DictInputMode.Add("quarterly (ecs/ si)", "3");
            DictInputMode.Add("half_yearly", "2");
            DictInputMode.Add("half-yearly", "2");
            DictInputMode.Add("quarterly", "3");
            DictInputMode.Add("monthly (ecs/ si)", "4");
            DictInputMode.Add("single", "5");
            string InputMode = DictInputMode[Mode].ToString();
            return InputMode;
        }

        public string DistributionChannelProtectShieldPlus(string DistChannel)
        {
            Dictionary<string, string> DictChannel = new Dictionary<string, string>();
            DictChannel.Add("corporate agent", "3");
            DictChannel.Add("corporate agency", "3");
            DictChannel.Add("direct marketing", "4");
            DictChannel.Add("online", "5");
            DictChannel.Add("broker", "6");
            DictChannel.Add("agency", "7");
            DictChannel.Add("web aggregator", "8");
            DictChannel.Add("insurance marketing firm", "9");


            string Distchannel = DictChannel[DistChannel].ToString();
            return Distchannel;
        }
    }
}

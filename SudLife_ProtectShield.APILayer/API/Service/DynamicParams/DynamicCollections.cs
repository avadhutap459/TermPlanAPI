namespace SudLife_ProtectShield.APILayer.API.Service.DynamicParams
{
    public class DynamicCollections
    {

        public string PremiumPaymentTermProtectShield(string PremiumPaymentTerm, string PolicyTerm)
        {
            if (PremiumPaymentTerm == "regular pay")
            {
                return PremiumPaymentTerm = PolicyTerm;
            }
            else
            {
                Dictionary<string, string> PremiumPayment = new Dictionary<string, string>();

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

        public string StaffPolicyProtectShield(string StaffDist)
        {
            Dictionary<string, string> DictStaffDist = new Dictionary<string, string>();

            DictStaffDist.Add("yes", "14");
            DictStaffDist.Add("no", "15");
            DictStaffDist.Add("sud life staff/family", "16");
            DictStaffDist.Add("direct sales team", "17");
            string Distchannel = DictStaffDist[StaffDist].ToString();
            return Distchannel;
        }

        public string DistributionChannelProtectShield(string DistChannel)
        {
            Dictionary<string, string> DictChannel = new Dictionary<string, string>();
            DictChannel.Add("corporate agent", "7");
            DictChannel.Add("corporate agency", "7");
            DictChannel.Add("direct  channel", "8");
            DictChannel.Add("agency", "9");
            DictChannel.Add("broker", "10");
            DictChannel.Add("insurance marketing firm", "11");
            DictChannel.Add("online", "12");
            DictChannel.Add("web aggregator", "13");


            string Distchannel = DictChannel[DistChannel].ToString();
            return Distchannel;
        }

        public string Smoker(string Smoker)
        {
            Dictionary<string, string> smokerOpt = new Dictionary<string, string>();

            smokerOpt.Add("non - smoker", "0");
            smokerOpt.Add("smoker", "1");

            string Smoke = smokerOpt[Smoker].ToString();
            return Smoke;

        }

        public string BenefitOption(string BenefitOpt)
        {
            Dictionary<string, string> BenefitOpts = new Dictionary<string, string>();

            BenefitOpts.Add("life cover", "1");
            BenefitOpts.Add("life cover with return of premium", "2");
            BenefitOpts.Add("life cover with critical illness", "3");
            string BenefitOp = BenefitOpts[BenefitOpt].ToString();
            return BenefitOp;

        }

        public string PayoutOptionProtectShield(string PayoutOpt)
        {
            Dictionary<string, string> PayoutOption = new Dictionary<string, string>();

            PayoutOption.Add("lump sum", "4");
            PayoutOption.Add("monthly income", "5");
            PayoutOption.Add("lump-sum plus monthly income", "6");

            string Payout = PayoutOption[PayoutOpt].ToString();
            return Payout;

        }
    }
}

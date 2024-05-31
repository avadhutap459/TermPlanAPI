namespace Sudlife_SaralJeevan.APILayer.API.Service.DynamicParams
{
    public class DynamicCollections
    {
        public string DistributionChannelSaralJeevanBima(string DistChannel)
        {
            Dictionary<string, string> DictChannel = new Dictionary<string, string>();
            DictChannel.Add("corporate agent", "1");
            DictChannel.Add("corporate agency", "1");
            DictChannel.Add("direct  channel", "2");
            DictChannel.Add("agency", "3");
            DictChannel.Add("broker", "4");
            DictChannel.Add("insurance marketing firm", "5");
            DictChannel.Add("online", "10");

            string Distchannel = DictChannel[DistChannel].ToString();
            return Distchannel;
        }

        public string StaffPolicySaralJeevanBima(string StaffDist)
        {
            Dictionary<string, string> DictStaffDist = new Dictionary<string, string>();
            DictStaffDist.Add("corporate agency staff", "6");
            DictStaffDist.Add("yes", "6");
            DictStaffDist.Add("no", "7");
            DictStaffDist.Add("sud life staff/family", "8");
            DictStaffDist.Add("direct sales team", "9");
            DictStaffDist.Add("online- others", "11");

            string Distchannel = DictStaffDist[StaffDist].ToString();
            return Distchannel;
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

        public string StandardAgeProof(string SAP)
        {
            Dictionary<string, string> DictSAP = new Dictionary<string, string>();
            DictSAP.Add("yes", "1");
            DictSAP.Add("no", "0");
            string SAPs = DictSAP[SAP].ToString();
            return SAPs;
        }

        public string Gender(string Gendr)
        {
            Dictionary<string, string> DictGender = new Dictionary<string, string>();
            DictGender.Add("male", "M");
            DictGender.Add("female", "F");
            DictGender.Add("third gender", "T");
            DictGender.Add("trans", "T");
            DictGender.Add("others", "O");
            string Gen = DictGender[Gendr].ToString();
            return Gen;
        }
    }
}

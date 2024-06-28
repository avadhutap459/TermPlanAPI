namespace SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel
{
    public class ClsRequest
    {
        public string AuthToken { get; set; } = string.Empty;
        public string TransId { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string EncryptReqSignValue {  get; set; }=string.Empty;
    }
}

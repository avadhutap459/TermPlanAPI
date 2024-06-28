namespace SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel
{
    public class ClsResponse
    {
        public string encryptResponseSignValue { get; set; } = string.Empty;
        public string checkSum {  get; set; }=string.Empty;
    }

    public class ClsBadResponseM
    {
        public ClsBadResponseM(string message, int status, string data, bool success, string transId, string timeStamp)
        {
            this.message = message;
            this.status = status;
            this.data = data;
            this.success = success;
            TransId = transId;
            this.timeStamp = timeStamp;
        }

        public string message { get; set; } = string.Empty;
        public int status { get; set; }
        public string data { get; set; } = string.Empty;
        public bool success { get; set; } 
        public string TransId { get; set; } = string.Empty;
        public string timeStamp { get; set; } = string.Empty;
    }
}

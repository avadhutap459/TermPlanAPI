namespace SudLife_Premiumcalculation.APILayer.API.Model.ServiceModel
{
    public class ClsSource
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime LastModifiedAt { get; set; }
        public string PrivateFilePath { get; set; } = string.Empty;
        public string PrivateFilePassword { get; set; } = string.Empty;
        public string PublicFilePath { get; set; } = string.Empty;
        public string EncryptDecryptPassword { get; set; } = string.Empty;
        public string secretkeyfortoken { get; set; } = string.Empty;
        public string secretkeyforchecksum { get; set; } = string.Empty;
    }

    public enum enumsource
    {
        BOI = 1
    }

    public enum enumsecuritythread
    {
        DecryptToken = 1,
        ValidateToken = 2,
        ValidateDigitalSign = 3,
        DecryptRequest = 4,
        ValidateChecksum = 5,
        EncryptResponse = 6,
        DigitalSign = 7,
        ChecksumGeneration = 8
    }
}

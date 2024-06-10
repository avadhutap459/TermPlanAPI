
namespace SudLife_MasterField.APILayer.API.Model
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
    }
}

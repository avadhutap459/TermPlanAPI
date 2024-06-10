using System.ComponentModel.DataAnnotations;

namespace Sudlife_SaralJeevan.APILayer.API.Model
{
    public class EncRequest
    {

        [Required]
        public string EncryptReqSignValue { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public string TransId { get; set; }

        [Required]
        public string AuthToken { get; set; }

      

    }
}

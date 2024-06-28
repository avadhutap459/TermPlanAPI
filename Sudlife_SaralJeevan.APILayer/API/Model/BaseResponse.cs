using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Sudlife_SaralJeevan.APILayer.API.Model
{
    public class BaseResponse
    {
        [Required]
        [StringLength(255, MinimumLength = 0)]
        public string Message { get; set; } 

        [JsonProperty("data")]
        public string Data { get; set; } = string.Empty;

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public bool IsSuccess { get; set; }
    }
}

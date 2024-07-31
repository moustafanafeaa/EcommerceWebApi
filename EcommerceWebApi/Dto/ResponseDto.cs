using System.Text.Json.Serialization;

namespace EcommerceWebApi.Dto
{
    public class ResponseDto
    {
        public string? Message { get; set; }
        public bool IsSucceeded { get; set; }
        public object? Model { get; set; }
        [JsonIgnore]
        public ICollection<object>? Models { get; set; }
    }
}

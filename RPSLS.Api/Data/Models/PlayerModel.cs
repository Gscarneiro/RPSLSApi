using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;
using System.Text.Json.Serialization;

namespace RPSLS.Api.Data.Models
{
    public class PlayerModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public static PlayerModel? FromDTO(Player dto)
        {
            return dto != null ? new PlayerModel() {
                Id = dto.Id,
                Name = dto.Name,
            } : null;
        }
    }
}
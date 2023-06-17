using System.Text.Json.Serialization;

namespace dotnet_pokemon.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Healer = 3,
        Archer = 4
    }
}
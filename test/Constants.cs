using System.Text.Json.Serialization;

namespace dotnet_rpg.Tests
{
    public class Constants
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum InvalidRpgClasses
        {
            Knnight =  4,
            Notamage = 5
        }
    }
}

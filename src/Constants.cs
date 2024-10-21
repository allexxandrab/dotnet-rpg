using System.Text.Json.Serialization;

namespace dotnet_rpg
{
    public class Constants
    {
        public class ValidationLimits
        {
            public const int IntMaxValue = 10;
            public const int NameMinValue = 2;
            public const int NameMaxValue = 30;
            public const int HitPointsMinValue = 1;
            public const int HitPointsMaxValue = 100;
        }

        public class Test
        {
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public enum InvalidRpgClasses
            {
                Knnight = 4,
                Notamage = 5
            }
        }
    }
}

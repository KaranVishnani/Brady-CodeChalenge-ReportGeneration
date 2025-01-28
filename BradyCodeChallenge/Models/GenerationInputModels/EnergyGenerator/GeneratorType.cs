using System.Reflection;

namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    public enum GeneratorType
    {
        None = 0,

        [StringValue("Offshore Wind")]
        OFFSHORE_WIND = 1,

        [StringValue("Onshore Wind")]
        ONSHORE_WIND = 2,

        [StringValue("Gas")]
        GAS = 3,

        [StringValue("Coal")]
        COAL = 4,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; }

        public StringValueAttribute(string stringValue)
        {
            StringValue = stringValue;
        }
    }

    public class EnumHelper
    {
        public static string GetStringValue(Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            StringValueAttribute? attribute = (StringValueAttribute?)Attribute.GetCustomAttribute(field, typeof(StringValueAttribute));
            return attribute?.StringValue ?? value.ToString();
        } 
    }
}

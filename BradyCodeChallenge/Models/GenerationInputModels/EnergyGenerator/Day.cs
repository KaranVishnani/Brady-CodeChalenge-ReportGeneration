using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    public class Day
    {
        [XmlElement("Date")]
        public string DateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTimeOffset Date
        {
            get => DateTimeOffset.Parse(DateString);
            set => DateString = value.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        [XmlElement("Energy")]
        public double Energy { get; set; }

        [XmlElement("Price")]
        public double Price { get; set; }
    }
}

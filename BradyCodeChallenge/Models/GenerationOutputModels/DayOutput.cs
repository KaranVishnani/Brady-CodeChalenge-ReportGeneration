using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationOutputModels
{
    public class DayOutput
    {
        [XmlElement]
        public string Name { get; set; } = string.Empty;

        [XmlElement("Date")]
        public string DateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTimeOffset Date
        {
            get => DateTimeOffset.Parse(DateString);
            set => DateString = value.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        [XmlElement("Emission")]
        public string EmissionString
        {
            get { return (EmissionDouble % 1 == 0) ? EmissionDouble.ToString("F0") : EmissionDouble.ToString("F9"); }
            set { EmissionDouble = double.Parse(value); }
        }

        [XmlIgnore]
        public double EmissionDouble { get; set; }

        public DayOutput() { }
        
        public DayOutput(string name, DateTimeOffset date, double emission)
        {
            Name = name;
            Date = date;
            EmissionDouble = Math.Round(emission, 9);
        }
    }
}
using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationOutputModels
{
    public class Generator
    {
        [XmlElement]
        public string Name { get; set; } = string.Empty;

        [XmlElement("Total")]
        public string TotalString
        {
            get { return (TotalDouble % 1 == 0) ? TotalDouble.ToString("F0") : TotalDouble.ToString("F9"); }
            set { TotalDouble = double.Parse(value); }
        }

        [XmlIgnore]
        public double TotalDouble { get; set; }

        public Generator() { }

        public Generator(string name, double total)
        {
            Name = name;
            TotalDouble = Math.Round(total, 9);
        }
    }
}
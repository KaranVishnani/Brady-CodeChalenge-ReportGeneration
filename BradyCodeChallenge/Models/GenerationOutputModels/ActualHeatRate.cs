using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationOutputModels
{
    public class ActualHeatRate
    {
        [XmlElement]
        public string Name { get; set; } = string.Empty;

        [XmlElement("HeatRate")]
        public string HeatRateString
        {
            get { return (HeatRateDouble % 1 == 0) ? HeatRateDouble.ToString("F0"): HeatRateDouble.ToString("F9"); }
            set { HeatRateDouble = double.Parse(value); }
        }

        [XmlIgnore]
        public double HeatRateDouble { get; set; }

        public ActualHeatRate() { }

        public ActualHeatRate(string name, double heatRate)
        {
            Name = name;
            HeatRateDouble = Math.Round(heatRate, 9);
        }
    }
}
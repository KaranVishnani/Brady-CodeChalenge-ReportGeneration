using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationOutputModels
{
    [XmlRoot]
    public class GenerationOutput
    {
        [XmlArray("Totals")]
        [XmlArrayItem("Generator")]
        public List<Generator> Generators { get; set; } = new List<Generator>();

        [XmlArray("MaxEmissionGenerators")]
        [XmlArrayItem("Day")]
        public List<DayOutput> Day { get; set; } = new List<DayOutput>();

        [XmlArray("ActualHeatRates")]
        [XmlArrayItem("ActualHeatRate")]
        public List<ActualHeatRate> ActualHeatRate { get; set; } = new List<ActualHeatRate>();

        public GenerationOutput()
        {
        }
    }
}

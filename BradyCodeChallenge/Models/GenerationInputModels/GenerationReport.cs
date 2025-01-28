using BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator;
using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationInputModels
{
    [XmlRoot("GenerationReport")]
    public class GenerationReport
    {
        [XmlArray("Wind")]
        [XmlArrayItem("WindGenerator")]
        public List<WindGenerator> Wind { get; set; } = new List<WindGenerator>();

        [XmlArray("Gas")]
        [XmlArrayItem("GasGenerator")]
        public List<GasGenerator> Gas { get; set; } = new List<GasGenerator>();

        [XmlArray("Coal")]
        [XmlArrayItem("CoalGenerator")]
        public List<CoalGenerator> Coal { get; set; } = new List<CoalGenerator>();

        [XmlIgnore]
        private List<EnergyGeneratorBase> _generator = new List<EnergyGeneratorBase>();

        public List<EnergyGeneratorBase> GetEnergies()
        {
            _generator.AddRange(Wind);
            _generator.AddRange(Gas);
            _generator.AddRange(Coal);
            return _generator;
        }
    }
}

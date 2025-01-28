using BradyCodeChallenge.ReportCalculations;
using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    [XmlInclude(typeof(WindGenerator))]
    [XmlInclude(typeof(GasGenerator))]
    [XmlInclude(typeof(CoalGenerator))]
    public abstract class EnergyGeneratorBase : IEnergyGenerator
    {
        [XmlElement]
        public string Name { get; set; } = string.Empty;

        [XmlArray("Generation")]
        [XmlArrayItem("Day")]
        public List<Day> Days { get; set; } = new List<Day>();

        [XmlIgnore]
        public abstract GeneratorType Type { get; set; }

        public void Accept(IEnergyCalculator calculator, params object[] args)
        {

            calculator.Calculate(this, args);
        }
    }
}
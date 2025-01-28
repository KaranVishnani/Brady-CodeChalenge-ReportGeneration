using BradyCodeChallenge.ReportCalculations;
using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    [XmlType("WindGenerator")]
    public class WindGenerator : EnergyGeneratorBase
    {
        [XmlElement("Location")]
        public string Location { get; set; } = string.Empty;

        [XmlIgnore]
        public override GeneratorType Type
        { 
            get
            {
                if (Location == "Offshore")
                    return GeneratorType.OFFSHORE_WIND;
                if (Location == "Onshore")
                    return GeneratorType.ONSHORE_WIND;
                else
                    return GeneratorType.None;
            }
            set { }
        }

        public new void Accept(IEnergyCalculator calculator, params object[] args)
        {
            calculator.Calculate(this, args);
        }
    }
}

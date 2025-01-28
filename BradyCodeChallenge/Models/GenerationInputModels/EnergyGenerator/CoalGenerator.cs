using BradyCodeChallenge.ReportCalculations;
using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    [XmlType("CoalGenerator")]
    public class CoalGenerator : EmissionEnergyGeneratorBase
    {
        [XmlElement("TotalHeatInput")]
        public double TotalHeatInput { get; set; }

        [XmlElement("ActualNetGeneration")]
        public double ActualNetGeneration { get; set; }

        [XmlIgnore]
        public override GeneratorType Type
        {
            get { return GeneratorType.COAL; }
            set { }
        }

        public new void Accept(IEmissionEnergyCalculator calculator, params object[] args)
        {
            calculator.Calculate(this, args);
        }
    }
}

using BradyCodeChallenge.ReportCalculations;
using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    [XmlType("GasGenerator")]
    public class GasGenerator : EmissionEnergyGeneratorBase
    {
        [XmlIgnore]
        public override GeneratorType Type
        {
            get { return GeneratorType.GAS; }
            set { }
        }

        public new void Accept(IEmissionEnergyCalculator calculator, params object[] args)
        {
            calculator.Calculate(this, args);
        }
    }
}

using BradyCodeChallenge.ReportCalculations;
using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    public abstract class EmissionEnergyGeneratorBase : EnergyGeneratorBase
    {
        [XmlElement("EmissionsRating")]
        public double EmissionsRating { get; set; }
        public void Accept(IEmissionEnergyCalculator calculator, params object[] args)
        {
            calculator.Calculate(this, args);
        }
    }
}

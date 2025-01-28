using BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator;

namespace BradyCodeChallenge.ReportCalculations
{
    public interface IEmissionEnergyCalculator
    {
        void Calculate(EmissionEnergyGeneratorBase energy, params object[] args);
        void Calculate(CoalGenerator energy, params object[] args);
    }
}

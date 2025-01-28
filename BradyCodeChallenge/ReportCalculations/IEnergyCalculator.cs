using BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator;

namespace BradyCodeChallenge.ReportCalculations
{
    public interface IEnergyCalculator
    {
        void Calculate(EnergyGeneratorBase energy, params object[] args);
    }
}

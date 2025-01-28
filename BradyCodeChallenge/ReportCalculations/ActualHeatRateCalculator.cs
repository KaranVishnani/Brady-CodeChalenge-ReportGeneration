using BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator;
using BradyCodeChallenge.Models.GenerationOutputModels;
using log4net;

namespace BradyCodeChallenge.ReportCalculations
{
    public class ActualHeatRateCalculator : IEmissionEnergyCalculator
    {
        private GenerationOutput _output;
        private readonly ILog _log;

        public ActualHeatRateCalculator(GenerationOutput output, ILog log)
        {
            _output = output;
            _log = log;
        }

        public void Calculate(EmissionEnergyGeneratorBase energy, params object[] args)
        {
            try
            {
                double total = CalculationMethods.CalculateActualHeatRate((double)args[0], (double)args[1]);
                _output.ActualHeatRate.Add(new ActualHeatRate(energy.Name, total));
            }
            catch (Exception ex)
            {
                _log.Error($"Error in ActualHeatRate calculation for {energy.GetType()}.", ex);
            }
        }

        public void Calculate(CoalGenerator coal, params object[] args)
        {
            Calculate((EmissionEnergyGeneratorBase)coal, coal.TotalHeatInput, coal.ActualNetGeneration);
        }
    }
}

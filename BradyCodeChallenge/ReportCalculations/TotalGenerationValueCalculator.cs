using BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator;
using BradyCodeChallenge.Models.GenerationOutputModels;
using BradyCodeChallenge.Models.ReferenceDataModels;
using log4net;

namespace BradyCodeChallenge.ReportCalculations
{
    public class TotalGenerationValueCalculator : IEnergyCalculator
    {
        private GenerationOutput _output;
        private readonly FactorBase _valueFactor;
        private readonly ILog _log;

        public TotalGenerationValueCalculator(GenerationOutput output, FactorBase factor, ILog log)
        {
            _output = output;
            _valueFactor = factor;
            _log = log;
        }

        public void Calculate(EnergyGeneratorBase energy, params object[] args)
        {
            string valueFactorKey = (string)args[0];
            try
            {
                double total = 0;
                double factorValue = CalculationMethods.GetValue<double>(_valueFactor, valueFactorKey);
                foreach (var day in energy.Days)
                {
                    total += CalculationMethods.CalculateTotalGenerationValue(day.Energy, day.Price, factorValue);
                }
                _output.Generators.Add(new Generator(energy.Name, total));
            }
            catch (Exception ex)
            {
                _log.Error($"Error in TotalGenerationValue calculation for {energy.GetType()}.", ex);
            }
        }
    }
}

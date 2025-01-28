using BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator;
using BradyCodeChallenge.Models.GenerationOutputModels;
using BradyCodeChallenge.Models.ReferenceDataModels;
using log4net;

namespace BradyCodeChallenge.ReportCalculations
{
    public class HighestDailyEmissionsCalculator : IEmissionEnergyCalculator
    {
        private GenerationOutput _output;
        private readonly FactorBase _emissionFactor;
        private readonly ILog _log;

        public HighestDailyEmissionsCalculator(GenerationOutput output, FactorBase emissionFactor, ILog log)
        {
            _output = output;
            _emissionFactor = emissionFactor;
            _log = log;
        }

        public void Calculate(EmissionEnergyGeneratorBase energy, params object[] args)
        {
            string emissionFactorKey = (string)args[0];

            double total = 0;
            try
            {
                double factorValue = CalculationMethods.GetValue<double>(_emissionFactor, emissionFactorKey);
                foreach (var day in energy.Days)
                {
                    total = CalculationMethods.CalculateTotalGenerationValue(day.Energy, energy.EmissionsRating, factorValue);
                    DayOutput? doutput = _output.Day.Where(x => x.Date == day.Date).FirstOrDefault();
                    if (doutput != null)
                    {
                        if (doutput.EmissionDouble < total)
                        {
                            doutput.EmissionDouble = total;
                            doutput.Name = energy.Name;
                        }
                    }
                    else
                    {
                        _output.Day.Add(new DayOutput(energy.Name, day.Date, total));
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Error in HighestDailyEmissions calculation for {energy.GetType()}.", ex);
            }
        }

        public void Calculate(GasGenerator gas, params object[] args)
        {
            Calculate((EmissionEnergyGeneratorBase)gas, args);
        }

        public void Calculate(CoalGenerator coal, params object[] args)
        {
            Calculate((EmissionEnergyGeneratorBase)coal, args);
        }
    }
}

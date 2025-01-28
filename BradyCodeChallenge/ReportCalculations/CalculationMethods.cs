using System.Reflection;

namespace BradyCodeChallenge.ReportCalculations
{
    public class CalculationMethods
    {
        public static double CalculateTotalGenerationValue(double energy, double price, double valueFactor)
        {
            return energy * price * valueFactor;
        }

        public static double CalculateHighestDailyEmissions(double energy, double emissionRating, double emissionFactor)
        {
            return energy * emissionRating * emissionFactor;
        }

        public static double CalculateActualHeatRate(double totalHeatInput, double ActualNetGeneration)
        {
            if(ActualNetGeneration == 0)
            {
                throw new DivideByZeroException();
            }

            return totalHeatInput / ActualNetGeneration;
        }

        public static T? GetValue<T>(object obj, string variableName)
        {
            if(obj == null)
            {
                throw new Exception("Passed object is Null.");
            }

            Type type = obj.GetType();
            PropertyInfo? propertyInfo = type.GetProperty(variableName);

            if (propertyInfo == null)
            {
                return default;
            }

            return (T?)propertyInfo.GetValue(obj);
        }
    }
}

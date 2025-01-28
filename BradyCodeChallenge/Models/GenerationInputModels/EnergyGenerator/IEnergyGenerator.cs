namespace BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator
{
    public interface IEnergyGenerator
    {
        string Name { get; set; }
        List<Day> Days { get; set; }
    }
}
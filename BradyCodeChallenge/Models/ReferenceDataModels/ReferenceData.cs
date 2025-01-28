using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.ReferenceDataModels
{
    [XmlRoot]
    public class ReferenceData
    {
        [XmlArray("Factors")]
        [XmlArrayItem("EmissionsFactor", Type = typeof(EmissionsFactor))]
        [XmlArrayItem("ValueFactor", Type = typeof(ValueFactor))]
        public List<FactorBase> Factors { get; set; } = new List<FactorBase>();
    }
}

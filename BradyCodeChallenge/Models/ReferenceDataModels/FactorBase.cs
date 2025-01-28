using System.Xml.Serialization;

namespace BradyCodeChallenge.Models.ReferenceDataModels
{
    [XmlInclude(typeof(EmissionsFactor))]
    [XmlInclude(typeof(ValueFactor))]
    public class FactorBase
    {
        [XmlElement]
        public double High {  get; set; }

        [XmlElement]
        public double Medium {  get; set; }

        [XmlElement]
        public double Low { get; set; }
    }
}

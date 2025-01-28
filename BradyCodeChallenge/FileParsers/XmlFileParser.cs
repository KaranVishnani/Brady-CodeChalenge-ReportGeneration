using log4net;
using System.Xml.Serialization;

namespace BradyCodeChallenge.FileParsers
{
    public class XmlFileParser : IFileParser
    {
        private readonly ILog _log;
        public XmlFileParser(ILog log)
        {
            _log = log;
        }

        public T? LoadData<T>(string filePath)
        {
            _log.DebugFormat("Loading XML file: {0}.", filePath);

            try
            {
                var xmlContent = File.ReadAllText(filePath);
                using (TextReader reader = new StringReader(xmlContent))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T?)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error in Loading XML file.", ex);
                return default;
            }
        }

        public void SaveData<T>(string filePath, T obj)
        {
            _log.DebugFormat("Saving data to XML file: {0}.", filePath);
            if (obj == null)
            {
                _log.Warn("Null object when saving data.");
                return;
            }

            try
            {
                using (TextWriter writer = new StreamWriter(filePath, false))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, obj);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error in Saving to XML file.", ex);
            }
        }
    }
}

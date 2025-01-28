using BradyCodeChallenge.FileParsers;
using log4net;

namespace BradyCodeChallenge
{
    public class FileProcessor
    {
        private IFileParser _fileParser;
        private readonly ILog _log;

        public FileProcessor(IFileParser fileParser, ILog log)
        {
            _fileParser = fileParser;
            _log = log;
        }

        public T? LoadFile<T>(string file)
        {
            return _fileParser.LoadData<T>(file);
        }

        public void SaveResultReport<T>(string file, T obj)
        {
            _fileParser.SaveData<T>(file, obj);
        }
    }
}

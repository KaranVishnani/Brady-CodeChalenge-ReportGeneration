using log4net;

namespace BradyCodeChallenge.FileMonitors
{
    public class FileSystemWatcherMonitor : IFileMonitor
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly ILog _log;

        public event FileSystemEventHandler FileCreated;

        public FileSystemWatcherMonitor(string filePath, string filter, ILog log)
        {
            _fileSystemWatcher = new FileSystemWatcher(filePath, filter);
            _fileSystemWatcher.Created += (sender, args) => FileCreated?.Invoke(sender, args);
            _log = log;
        }

        public void Start()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
        }
    }
}

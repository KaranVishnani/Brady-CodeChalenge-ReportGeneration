namespace BradyCodeChallenge.FileMonitors
{
    public interface IFileMonitor
    {
        void Start();
        event FileSystemEventHandler FileCreated;
    }
}

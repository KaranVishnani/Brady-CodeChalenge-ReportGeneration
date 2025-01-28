namespace BradyCodeChallenge.FileParsers
{
    public interface IFileParser
    {
        T? LoadData<T>(string filePath);
        void SaveData<T>(string filePath, T obj);
    }
}

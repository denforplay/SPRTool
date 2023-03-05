namespace SPR.FileWorker.Interfaces
{
    public interface IFileReader
    {
        T Read<T>(string filepath);
        Task<T> ReadAsync<T>(string filepath);
    }
}
namespace SPR.FileWorker.Interfaces
{
    public interface IFileWriter
    {
        void Write<T>(string filepath, T objectToWrite);
        Task WriteAsync<T>(string filepath, T objectToWrite);
    }
}

using Newtonsoft.Json;
using SPR.FileWorker.Interfaces;

namespace SPR.FileWorker
{
    public class FileWorker : IFileReader, IFileWriter
    {
        public T Read<T>(string filepath)
        {
            T? readedObject = default;
            if (File.Exists(filepath))
            {
                using (StreamReader file = File.OpenText(filepath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    readedObject = (T?)serializer.Deserialize(file, typeof(T));
                }
            }

            if (readedObject is null)
            {
                throw new ArgumentNullException(nameof(readedObject));
            }

            return readedObject;
        }

        public Task<T> ReadAsync<T>(string filepath)
        {
            throw new NotImplementedException();
        }

        public void Write<T>(string filepath, T objectToWrite)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            using (StreamWriter file = File.CreateText(filepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, objectToWrite);
            }
        }

        public async Task WriteAsync<T>(string filepath, T objectToWrite)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            using (StreamWriter file = File.CreateText(filepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, objectToWrite);
            }
        }
    }
}

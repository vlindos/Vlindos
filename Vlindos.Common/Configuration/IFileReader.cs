namespace Vlindos.Common.Configuration
{
    public interface IFileReaderFactory<T>
    {
        IFileReader<T> GetFileReader(string filePath);
    }

    public interface IFileReader<T> : IReader<T>
    {
    }
}
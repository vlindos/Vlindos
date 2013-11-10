namespace Vlindos.Common.Configuration
{
    public interface IReader<T>
    {
        bool Read(out T configuration);
    }
}
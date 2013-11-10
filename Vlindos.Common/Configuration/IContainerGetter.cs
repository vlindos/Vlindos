namespace Vlindos.Common.Configuration
{
    public interface IContainerGetter<T>
    {
        bool GetContainer(IReader<T> reader, out IContainer<T> container);
    }
}

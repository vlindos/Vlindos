using Vlindos.Common.Streams;

namespace Framework.Web.Tools
{
    public interface IFormDataReader
    {
        string ReadFrom(IInputStream inputStream, string password);
    }
}
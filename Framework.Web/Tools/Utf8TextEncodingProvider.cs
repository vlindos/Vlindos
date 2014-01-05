using System.Text;

namespace Framework.Web.Tools
{
    public interface ITextEncodingProvider
    {
        Encoding Endcoding { get; }
    }

    public class Utf8TextEncodingProvider : ITextEncodingProvider
    {
        public Encoding Endcoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
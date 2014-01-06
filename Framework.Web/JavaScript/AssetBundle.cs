using System.Text;

namespace Framework.Web.JavaScript
{
    public class AssetBundle
    {
        public string Url { get; set; }
        public byte[] Data { get; set; }
        public Encoding Encoding { get; set; }
        public string HtmlNodeText { get; set; }
        public string ContentType { get; set; }
    }
}
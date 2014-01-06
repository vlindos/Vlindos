namespace Framework.Web.Tools
{
    public class DataEndpoint
    {
        public string Url { get; set; }
        public string Encoding { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }

    public interface IDynamicDataEndpointsManager
    {
        void SetDataEndpoint(DataEndpoint dataEndpoint);
    }
}
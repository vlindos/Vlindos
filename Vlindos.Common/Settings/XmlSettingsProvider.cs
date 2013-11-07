using System.Xml;

namespace Vlindos.Common.Settings
{
    public interface IXmlSettingsProviderFactory
    {
        IXmlSettingsProvider GetXmlSettingsProvider(XmlNode element);
    }

    public interface IXmlSettingsProvider : ISettingsProvider
    {
        XmlNodeList GetNodesForKey(string path);
    }

    public class XmlSettingsProvider : IXmlSettingsProvider
    {
        private readonly XmlNode _rootXmlElement;

        public XmlSettingsProvider(XmlNode rootXmlElement)
        {
            _rootXmlElement = rootXmlElement;
        }

        public string GetValueForKey(string path)
        {
            var xmlNode = _rootXmlElement.SelectSingleNode(path);
            if (xmlNode == null) return null;
            return xmlNode.Value;
        }

        public XmlNodeList GetNodesForKey(string path)
        {
            var xmlNodeList = _rootXmlElement.SelectNodes(path);
            return xmlNodeList;
        }
    }
}
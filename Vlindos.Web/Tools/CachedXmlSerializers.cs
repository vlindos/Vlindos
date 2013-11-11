using System;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace Users.Common.Tools
{
    public interface ICachedResponseXmlSerializers
    {
        XmlSerializer GetCachedXmlSerializer(Type modelType);
    }

    public class CachedResponseXmlSerializers : ICachedResponseXmlSerializers
    {
        private readonly ConcurrentDictionary<Type, XmlSerializer> _cachedSerializers;

        public CachedResponseXmlSerializers()
        {
            _cachedSerializers = new ConcurrentDictionary<Type, XmlSerializer>();
        }

        public XmlSerializer GetCachedXmlSerializer(Type modelType)
        {
            var serializer = _cachedSerializers.GetOrAdd(modelType, x => new XmlSerializer(x));
            return serializer;
        }
    }
}

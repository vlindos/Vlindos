using System.Collections.Generic;
using System.Text;
using Framework.Web.Application.HttpEndpoint;
using Vlindos.Common.Extensions.IEnumerable;

namespace Framework.Web.JavaScript
{
    public interface IJavascriptSourcePerformer : IPerformer<string>
    {
    }

    public class JavascriptSourcePerformer : IJavascriptSourcePerformer
    {
        private readonly IJavascriptCompressor _javascriptCompressor;
        private readonly IEnumerable<IJavascriptProvider> _javascriptProviders;
        private string _cache;
        private readonly object _lockObject;
        private readonly bool _compressAssets;

        public JavascriptSourcePerformer(
            IJavascriptCompressor javascriptCompressor, 
            IEnumerable<IJavascriptProvider> javascriptProviders, 
            IAssetsCompressSettingProvider assetsCompressSettingProvider)
        {
            _javascriptCompressor = javascriptCompressor;
            _javascriptProviders = javascriptProviders;
            _compressAssets = assetsCompressSettingProvider.CompressAssets;
            _lockObject = new object();
        }

        public string Perform()
        {   
            if (_cache != null)
            {
                return _cache;
            }
            lock (_lockObject)
            {
                var sb = new StringBuilder();

                if (_compressAssets)
                {
                    _javascriptProviders.ForEach(x => sb.Append(_javascriptCompressor.CompressJavascript(x.GetJavascript())));
                }
                else
                {
                    _javascriptProviders.ForEach(x => sb.Append(x.GetJavascript()));
                }
                _cache = sb.ToString();

                return _cache;
            }
        }
    }
}
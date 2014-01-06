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
        private readonly IJsTransformer _jsTransformer;
        private readonly IEnumerable<IJavascriptProvider> _javascriptProviders;
        private string _cache;
        private readonly object _lockObject;

        public JavascriptSourcePerformer(
            IJsTransformer jsTransformer, 
            IEnumerable<IJavascriptProvider> javascriptProviders,
            IAssetsTransformersManager assetsTransformersManager)
        {
            _jsTransformer = jsTransformer;
            _javascriptProviders = javascriptProviders;
            _jsTransformer = assetsTransformersManager.GetJsTransformer();
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
                _javascriptProviders.ForEach(x => sb.Append(x.GetJavascript()));
                _cache = _jsTransformer.TransformJsContent(sb.ToString());
                return _cache;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Framework.Web.Application;
using Framework.Web.Tools;

namespace Framework.Web.JavaScript
{
    
    public interface IAssetsRendererFactory
    {
        IAssetsRenderer GetAssetsRenderer(HttpContext httpContext, string baseUrl);
    }

    public interface IAssetsRenderer : IDisposable
    {
    }

    public class AssetsRenderer : IAssetsRenderer
    {
        private readonly IStringResponseWritter _stringResponseWritter;
        private readonly IJavascriptCompressor _javascriptCompressor;
        private readonly ICssCompressor _cssCompressor;
        private readonly IDynamicDataEndpointsManager _dynamicDataEndpointsManager;
        private readonly HttpContext _httpContext;
        private readonly string _baseUrl;
        private readonly string _documentRootFilePath;
        private readonly bool _compressAssets;
        private readonly List<string> _javascriptFilePaths;
        private readonly List<string> _cssFilePaths;
        private Encoding _encoding;

        public AssetsRenderer(
            IDocumentRootProvider documentRootProvider, 
            IStringResponseWritter stringResponseWritter, 
            IAssetsCompressSettingProvider assetsCompressSettingProvider,
            IJavascriptCompressor javascriptCompressor,
            ICssCompressor cssCompressor,
            IDynamicDataEndpointsManager dynamicDataEndpointsManager,
            HttpContext httpContext,
            ITextEncodingProvider textEncodingProvider,
            string baseUrl)
        {
            _stringResponseWritter = stringResponseWritter;
            _javascriptCompressor = javascriptCompressor;
            _cssCompressor = cssCompressor;
            _dynamicDataEndpointsManager = dynamicDataEndpointsManager;
            _httpContext = httpContext;
            _encoding = textEncodingProvider.Endcoding;
            _baseUrl = baseUrl;
            _documentRootFilePath = documentRootProvider.Filepath;
            _compressAssets = assetsCompressSettingProvider.CompressAssets;
            _javascriptFilePaths = new List<string>();
            _cssFilePaths = new List<string>();
        }

        public void RenderJavascript(string filePath)
        {
            _javascriptFilePaths.Add(filePath);
        }

        public void RenderCss(string filePath)
        {
            _cssFilePaths.Add(filePath);
        }

        public void Dispose()
        {
            if (!_compressAssets)
            {
                foreach (var javascriptFilePath in _javascriptFilePaths)
                {
                    _stringResponseWritter.WriteResponse(_httpContext, javascriptFilePath);
                }
                foreach (var cssFilePath in _cssFilePaths)
                {
                    _stringResponseWritter.WriteResponse(_httpContext, cssFilePath);
                }
                return;
            }
         
            if (_cssFilePaths.Count != 0)
            {
                var url = _baseUrl + ".css";
                _stringResponseWritter.WriteResponse(_httpContext, url);
                var sb = new StringBuilder();
                _cssFilePaths.ForEach(x => sb.Append(File.ReadAllText(Path.Combine(_documentRootFilePath, x))));
                var compressedContents = _cssCompressor.CompressCss(sb.ToString());

                _dynamicDataEndpointsManager.SetDataEndpoint(new DataEndpoint
                {
                    Url = url,
                    Encoding = _encoding.EncodingName,
                    ContentType = "text/css",
                    Data = _encoding.GetBytes(compressedContents)
                });
            }
            if (_javascriptFilePaths.Count != 0)
            {
                var url = _baseUrl + ".js";
                _stringResponseWritter.WriteResponse(_httpContext, url);
                var sb = new StringBuilder();
                _javascriptFilePaths.ForEach(
                    x => sb.Append(File.ReadAllText(Path.Combine(_documentRootFilePath, x))));
                var compressedContents = _javascriptCompressor.CompressJavascript(sb.ToString());

                _dynamicDataEndpointsManager.SetDataEndpoint(new DataEndpoint
                {
                    Url = url,
                    Encoding = _encoding.EncodingName,
                    ContentType = "text/javascript",
                    Data = _encoding.GetBytes(compressedContents)
                });
            }
        }
    }
}
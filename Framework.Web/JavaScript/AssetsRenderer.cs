using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Web.HtmlPages;
using Framework.Web.Tools;

namespace Framework.Web.JavaScript
{
    public interface IAssetsRendererFactory
    {
        IAssetsRenderer GetAssetsRenderer(IHtmlPageRenderer htmlPageRenderer, string baseUrl);
    }

    public interface IAssetsRenderer : IDisposable
    {
    }

    public class AssetsRenderer : IAssetsRenderer
    {
        private readonly IHtmlPageRenderer _htmlPageRenderer;
        private readonly IDynamicDataEndpointsManager _dynamicDataEndpointsManager;
        private readonly IAssetsTransformersManager _assetsTransformersManager;
        private readonly IAssetsRenderWarehouse _assetsRenderWarehouse;
        private readonly string _baseUrl;
        private readonly List<string> _jsFilePaths;
        private readonly List<string> _cssFilePaths;
        private readonly bool _firstRun;
        public AssetsRenderer(
            IHtmlPageRenderer htmlPageRenderer,
            string baseUrl,
            IDynamicDataEndpointsManager dynamicDataEndpointsManager,
            IAssetsTransformersManager assetsTransformersManager,
            IAssetsRenderWarehouse assetsRenderWarehouse)
        {
            _htmlPageRenderer = htmlPageRenderer;
            _dynamicDataEndpointsManager = dynamicDataEndpointsManager;
            _assetsTransformersManager = assetsTransformersManager;
            _assetsRenderWarehouse = assetsRenderWarehouse;
            _baseUrl = baseUrl;
            _jsFilePaths = new List<string>();
            _cssFilePaths = new List<string>();
            _firstRun = assetsRenderWarehouse.AcquireAssetsGroup(baseUrl);
        }

        public void RenderJs(string filePath)
        {
            if (!_firstRun) return;

            _jsFilePaths.Add(filePath);
        }

        public void RenderCss(string filePath)
        {
            if (!_firstRun) return;

            _cssFilePaths.Add(filePath);
        }

        public void Dispose()
        {
            foreach (var bundle in _assetsTransformersManager.GetJsTransformer()
                                                             .TransformJsFiles(_baseUrl, _jsFilePaths)
                                                             .ToArray())
            {
                _dynamicDataEndpointsManager.SetDataEndpoint(new DataEndpoint
                {
                    Url = bundle.Url,
                    Encoding = bundle.Encoding.EncodingName,
                    ContentType = bundle.ContentType,
                    Data = bundle.Data
                });
                _assetsRenderWarehouse.AddBundle(_baseUrl, bundle, AssetType.Js);
            }
            foreach (var bundle in _assetsTransformersManager.GetCssTransformer()
                                                             .TransformCssFiles(_baseUrl, _cssFilePaths)
                                                             .ToArray())
            {
                _dynamicDataEndpointsManager.SetDataEndpoint(new DataEndpoint
                {
                    Url = bundle.Url,
                    Encoding = bundle.Encoding.EncodingName,
                    ContentType = bundle.ContentType,
                    Data = bundle.Data
                });
                _assetsRenderWarehouse.AddBundle(_baseUrl, bundle, AssetType.Css);
            }
            if (_firstRun) _assetsRenderWarehouse.CompleteRendering(_baseUrl);
            _assetsRenderWarehouse.WaitRenderToComplete(_baseUrl);
            foreach (var bundle in _assetsRenderWarehouse.Bundles(_baseUrl, AssetType.Js))
            {
                _htmlPageRenderer.Render(bundle.HtmlNodeText);
            }
            foreach (var bundle in _assetsRenderWarehouse.Bundles(_baseUrl, AssetType.Css))
            {
                _htmlPageRenderer.Render(bundle.HtmlNodeText);
            }
        }
    }
}
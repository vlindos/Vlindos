using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Framework.Web.Tools;

namespace Framework.Web.Assets
{
    public interface ICssTransformer
    {
        IEnumerable<AssetBundle> TransformCssFiles(string baseUrl, IEnumerable<string> filePaths);
        string TransformCssContent(string content);
    }

    public interface INoTransformCssTransformer : ICssTransformer
    {
    }

    public class NoTransformCssTransformer : INoTransformCssTransformer
    {
        private readonly Encoding _encoding;
        private readonly string _documentFilePath;

        public NoTransformCssTransformer(ITextEncodingProvider textEncodingProvider, IDocumentRootProvider documentRootProvider)
        {
            _encoding = textEncodingProvider.Endcoding;
            _documentFilePath = documentRootProvider.Filepath;
        }

        public IEnumerable<AssetBundle> TransformCssFiles(string baseUrl, IEnumerable<string> filePaths)
        {
            return filePaths.Select(filePath => new AssetBundle
            {
                ContentType = "text/css",
                Data = _encoding.GetBytes(File.ReadAllText(Path.Combine(_documentFilePath, filePath))),
                Encoding = _encoding,
                HtmlNodeText = string.Format("<script src='{0}' type='media/css' />", filePath),
                Url = filePath
            });
        }

        public string TransformCssContent(string content)
        {
            return content;
        }
    }
}
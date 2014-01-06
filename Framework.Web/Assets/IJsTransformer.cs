using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Framework.Web.Tools;

namespace Framework.Web.Assets
{
    public interface IJsTransformer
    {
        IEnumerable<AssetBundle> TransformJsFiles(string baseUrl, IEnumerable<string> filePaths);
        string TransformJsContent(string content);
    }

    public interface INoTransformJsTransformer : IJsTransformer
    {
    }

    public class NoTransformJsTransformer : INoTransformJsTransformer
    {
        private readonly Encoding _encoding;
        private readonly string _documentFilePath;

        public NoTransformJsTransformer(ITextEncodingProvider textEncodingProvider, IDocumentRootProvider documentRootProvider)
        {
            _encoding = textEncodingProvider.Endcoding;
            _documentFilePath = documentRootProvider.Filepath;
        }

        public IEnumerable<AssetBundle> TransformJsFiles(string baseUrl, IEnumerable<string> filePaths)
        {
            return filePaths.Select(filePath => new AssetBundle
            {
                ContentType = "text/javascript",
                Data = _encoding.GetBytes(File.ReadAllText(Path.Combine(_documentFilePath, filePath))),
                Encoding = _encoding,
                HtmlNodeText = string.Format("<script src='{0}' type='script/javascript' />", filePath),
                Url = filePath
            });
        }

        public string TransformJsContent(string content)
        {
            return content;
        }
    }
}
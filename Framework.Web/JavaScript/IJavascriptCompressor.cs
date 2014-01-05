using System;

namespace Framework.Web.JavaScript
{
    public interface IJavascriptCompressor
    {
        string CompressJavascript(string jsContents);
    }

    public class JavascriptCompressor : IJavascriptCompressor
    {
        public string CompressJavascript(string jsContents)
        {
            throw new NotImplementedException();
        }
    }
}
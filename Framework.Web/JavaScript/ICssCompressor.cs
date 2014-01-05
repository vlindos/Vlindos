using System;

namespace Framework.Web.JavaScript
{
    public interface ICssCompressor
    {
        string CompressCss(string cssContents);
    }

    public class CssCompressor : ICssCompressor
    {
        public string CompressCss(string cssContents)
        {
            throw new NotImplementedException();
        }
    }
}
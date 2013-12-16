using System;
using System.IO;
using Framework.Web.Models;

namespace Framework.Web.HtmlPages
{
    public class HtmlResponseWritter<TRequest, TResponse> : IHtmlResponseWritter<TRequest, TResponse>
        where TResponse : IHtmlResponse
    {
        private readonly IPagesRepositoryProvider _pagesRepositoryProvider;
        private readonly IHtmlPageManager _htmlPageManager;
        private readonly IHtmlPageWriteTimeoutProvider _htmlPageWriteTimeoutProvider;

        public HtmlResponseWritter(
            IPagesRepositoryProvider pagesRepositoryProvider, 
            IHtmlPageManager htmlPageManager, 
            IHtmlPageWriteTimeoutProvider htmlPageWriteTimeoutProvider)
        {
            _pagesRepositoryProvider = pagesRepositoryProvider;
            _htmlPageManager = htmlPageManager;
            _htmlPageWriteTimeoutProvider = htmlPageWriteTimeoutProvider;
        }

        public void WriteResponse(IHttpRequest<TRequest> httpRequest, IHttpResponse<TResponse> httpResponse)
        {
            var pageLocation = Path.Combine(_pagesRepositoryProvider.Path, httpResponse.Response.PageName);
            var htmlPage = _htmlPageManager.GetHtmlPage(pageLocation);
            if (htmlPage == null)
            {
                throw new Exception();
            }
            while (true)
            {
                var bytes = htmlPage.Read();
                if (bytes.Length <= 0) return;
                // httpResponse.OutputStream.Write(bytes, _htmlPageWriteTimeoutProvider.Timeout);
            }
        }
    }
}
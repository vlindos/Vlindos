using System;
using Framework.Web.Application;
using Framework.Web.Models;

namespace Framework.Web.Service
{
    public class PerformerManager : IPerformerManager
    {
        public TPerformer GetPerformer<TPerformer, TResponse>(IHttpRequest request, IHttpResponse httpResponse) 
            where TPerformer : IRequestPerformer<TResponse>
        {
            throw new NotImplementedException();
        }
    }
}

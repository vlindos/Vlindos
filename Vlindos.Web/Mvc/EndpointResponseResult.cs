using System;
using System.Net;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Users.Common.Models.Endpoint;
using Users.Common.Tools;

namespace Users.Common.Mvc
{
    public interface IEndpointActionResultFactory
    {
        IEndpointActionResult<T> GetEndpointActionResult<T>() where T : IEndpointResponse;
    }

    public interface IEndpointActionResult<T> where T : IEndpointResponse
    {
        void SetHttpStatusCode(HttpStatusCode httpStatusCode);
        IResponseStreamWriter<T> ResponseStreamWriter { get; set; }
        T EndpointResponse { get; }
        ActionResult ActionResult { get; }
    }

    public class EndpointActionResult<T> : ActionResult, IEndpointActionResult<T>
        where T : IEndpointResponse
    {
        private readonly HttpResponseContext _httpResponseContext;
        private readonly T _endpointResponse;
        public EndpointActionResult()
        {
            _httpResponseContext = new HttpResponseContext();
            ResponseStreamWriter = ServiceLocator.Current.GetInstance<ISerializedResponseStreamWritter<T>>();
            _endpointResponse = Activator.CreateInstance<T>();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_httpResponseContext != null)
            {
                context.HttpContext.Response.StatusCode = (int)_httpResponseContext.HttpStatusCode;   
            }

            ResponseStreamWriter.WriteResponse(context, _endpointResponse);
        }

        public void SetHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            _httpResponseContext.HttpStatusCode = httpStatusCode;
        }

        public IResponseStreamWriter<T> ResponseStreamWriter { get; set; }


        public T EndpointResponse 
        {
            get
            {
                return _endpointResponse;
            }
        }

        public ActionResult ActionResult 
		{
            get { return this; }
        }
    }
}
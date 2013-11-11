using System;
using System.Net;
using Vlindos.Web.Models.Endpoint;
using Vlindos.Web.Tools;

namespace Vlindos.Web.Mvc
{
    //public interface IEndpointActionResultFactory
    //{
    //    IEndpointActionResult<T> GetEndpointActionResult<T>() where T : IEndpointResponse;
    //}

    //public interface IEndpointActionResult<T> where T : IEndpointResponse
    //{
    //    void SetHttpStatusCode(HttpStatusCode httpStatusCode);
    //    IResponseStreamWriter<T> ResponseStreamWriter { get; set; }
    //    T EndpointResponse { get; }
    //    ActionResult ActionResult { get; }
    //}

    //public class EndpointActionResult<T> : ActionResult, IEndpointActionResult<T>
    //    where T : IEndpointResponse
    //{
    //    private readonly HttpResponseContext _httpResponseContext;
    //    private readonly T _endpointResponse;
    //    public EndpointActionResult()
    //    {
    //        _httpResponseContext = new HttpResponseContext();
    //        ResponseStreamWriter = ServiceLocator.Current.GetInstance<ISerializedResponseStreamWritter<T>>();
    //        _endpointResponse = Activator.CreateInstance<T>();
    //    }

    //    public override void ExecuteResult(ControllerContext context)
    //    {
    //        if (_httpResponseContext != null)
    //        {
    //            context.HttpContext.Response.StatusCode = (int)_httpResponseContext.HttpStatusCode;   
    //        }

    //        ResponseStreamWriter.WriteResponse(context, _endpointResponse);
    //    }

    //    public void SetHttpStatusCode(HttpStatusCode httpStatusCode)
    //    {
    //        _httpResponseContext.HttpStatusCode = httpStatusCode;
    //    }

    //    public IResponseStreamWriter<T> ResponseStreamWriter { get; set; }


    //    public T EndpointResponse 
    //    {
    //        get
    //        {
    //            return _endpointResponse;
    //        }
    //    }

    //    public ActionResult ActionResult 
    //    {
    //        get { return this; }
    //    }
    //}
}
using Vlindos.Web.Models.Endpoint;

namespace Vlindos.Web.Tools
{
    //public interface ISerializedResponseStreamWritter<in T> : IResponseStreamWriter<T>
    //    where T : IEndpointResponse
    //{
    //}

    //// http://shiflett.org/blog/2011/may/the-accept-header
    //// http://stackoverflow.com/questions/267546/correct-http-header-for-json-file
    //// http://zoom.it/pages/api/formats/xml-vs-json-p
    //// http://docs.openstack.org/api/openstack-network/1.0/content/Request_Response_Types.html
    //public class SerializedResponseStreamWritter<T> : ISerializedResponseStreamWritter<T>
    //    where T : IEndpointResponse
    //{
    //    private readonly IAcceptHeaderPrioritizer _acceptHeaderPrioritizer;
    //    private readonly ICachedResponseXmlSerializers _cachedResponseXmlSerializers;

    //    public SerializedResponseStreamWritter(IAcceptHeaderPrioritizer acceptHeaderPrioritizer,
    //                                             ICachedResponseXmlSerializers cachedResponseXmlSerializers)
    //    {
    //        _acceptHeaderPrioritizer = acceptHeaderPrioritizer;
    //        _cachedResponseXmlSerializers = cachedResponseXmlSerializers;
    //    }

    //    protected void SerializeAsXml(ControllerContext context, T response)
    //    {
    //        context.HttpContext.Response.ContentType = "application/xml";
    //        var serializer = _cachedResponseXmlSerializers.GetCachedXmlSerializer(typeof(T));
    //        serializer.Serialize(context.HttpContext.Response.Output, response);
    //    }

    //    protected void SerializeAsJson(ControllerContext context, T reponse)
    //    {
    //        context.HttpContext.Response.ContentType = "application/json";
    //        var js = new Newtonsoft.Json.JsonSerializer();
    //        js.Serialize(context.HttpContext.Response.Output, reponse);
    //    }

    //    public void WriteResponse(ControllerContext controllerContext, T serviceResponse)
    //    {
    //        var queryFormat = controllerContext.HttpContext.Request.QueryString["format"];
    //        queryFormat = string.IsNullOrWhiteSpace(queryFormat) ? null : queryFormat.ToLowerInvariant();
    //        switch (queryFormat)
    //        {
    //            case "application/xml":
    //                SerializeAsXml(controllerContext, serviceResponse);
    //                return;
    //            case "application/json":
    //                SerializeAsJson(controllerContext, serviceResponse);
    //                return;
    //        }
    //        var acceptHeader = controllerContext.HttpContext.Request.Headers["Accept"];
    //        if (acceptHeader == null)
    //        {
    //            SerializeAsXml(controllerContext, serviceResponse);
    //            return;
    //        }
    //        foreach (var acceptToken in _acceptHeaderPrioritizer.Prioritize(acceptHeader))
    //        {
    //            switch (acceptToken)
    //            {
    //                case "application/xml":
    //                    {
    //                        SerializeAsXml(controllerContext, serviceResponse);
    //                        return;
    //                    }
    //                case "application/json":
    //                    {
    //                        SerializeAsJson(controllerContext, serviceResponse);
    //                        return;
    //                    }
    //            }
    //        }

    //        SerializeAsXml(controllerContext, serviceResponse);
    //    }
    //}
}
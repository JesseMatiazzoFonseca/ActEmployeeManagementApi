using Domain.Models.Response;
using Newtonsoft.Json;
using System.Net;

namespace API.Configuration
{
    public static class MiddlewareSetup
    {
        public class CustonMiddleware
        {
            private readonly RequestDelegate _next;
            public CustonMiddleware(RequestDelegate next)
            {
                _next = next;
            }
            public async Task Invoke(HttpContext httpContext)
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode != (int)HttpStatusCode.OK)
                {
                    var result = new BaseResponse<string>
                    {
                        StatusCode = httpContext.Response.StatusCode
                    };

                    if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        result.Message = "Não autorizado";
                    }

                    else if (httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                    {
                        result.Message = "Não possui permissão";
                    }

                    httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    httpContext.Response.ContentType = "application/json";

                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                        Formatting = Newtonsoft.Json.Formatting.Indented
                    }));
                }
            }

        }
        public static IApplicationBuilder UseMiddlewareSetup(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustonMiddleware>();
        }
    }
}

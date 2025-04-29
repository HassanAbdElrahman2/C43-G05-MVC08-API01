using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustmExeptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustmExeptionHandlerMiddleWare> _logger;

        public CustmExeptionHandlerMiddleWare(RequestDelegate next ,ILogger<CustmExeptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Somthing went Wrong");
                // httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError ;

                httpContext.Response.StatusCode = ex switch {
                    NotFoundException=>StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError

                };
                // httpContext.Response.ContentType = "application/json";
                var Error = new ErorrToReturn()
                {ErrorMessage=ex.Message,StatusCode = httpContext.Response.StatusCode };
                 await httpContext.Response.WriteAsJsonAsync( Error);
               
            }
        }
    }
}

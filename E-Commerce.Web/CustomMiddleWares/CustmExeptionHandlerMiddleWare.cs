using Azure;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.IdentityExceptions;
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

                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Somthing went Wrong");
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError ;
            var Response= new ErorrToReturn()
            {
                ErrorMessage = ex.Message
            };

            Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException=> StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException =>GetBadRequestErrors(badRequestException,Response),
                _ => StatusCodes.Status500InternalServerError

            };
            httpContext.Response.StatusCode=Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErorrToReturn response)
        {
          response.Errors=badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Responce = new ErorrToReturn()
                { StatusCode = StatusCodes.Status404NotFound, ErrorMessage = $"End Point{httpContext.Request.Path} is not found" };
                await httpContext.Response.WriteAsJsonAsync(Responce);
            }
        }
    }
}

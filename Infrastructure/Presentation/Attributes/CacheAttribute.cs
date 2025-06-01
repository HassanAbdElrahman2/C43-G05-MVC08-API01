using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int Time=90):ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create Key
            string Key = CreateKey(context.HttpContext.Request);
            // Search With Key
            var CacheService=context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var Result=await CacheService.GetAsync(Key);
            if(Result is not null)
            {
                context.Result=new ContentResult()
                { Content = Result, ContentType="application/json",StatusCode=StatusCodes.Status200OK };
                return;
            }
            var ExecutedContext=await next.Invoke();
            if (ExecutedContext.Result is OkObjectResult result)
                await CacheService.SetAsync(Key, result.Value, TimeSpan.FromSeconds(Time));

        }

        private string CreateKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path+'?');
            foreach (var item in request.Query.OrderBy(I=>I.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");

            }
            return Key.ToString();
        }
    }
}

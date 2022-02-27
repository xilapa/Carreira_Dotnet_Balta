using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FundamentosAspNet6.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IResourceFilter
{
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        // Do nothing
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        if(!context.HttpContext.Request.Headers.TryGetValue(Configuration.ApiKeyName, out var key))
        {
            context.Result = new ContentResult { StatusCode = (int)HttpStatusCode.Unauthorized };
            return;
        }

        if(key != Configuration.ApiKey)
            context.Result = new ContentResult { StatusCode = (int)HttpStatusCode.Forbidden };
    }
}
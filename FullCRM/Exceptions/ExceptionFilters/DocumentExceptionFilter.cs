using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace FullCRM.Exceptions.ExceptionFilters
{
    public class DocumentExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DocumentNotFoundException)
            {
                var exceptionMessage = context.Exception.Message;
                context.Result = new ContentResult
                {
                    Content = $"{exceptionMessage}"
                };
                context.HttpContext.Response.Headers.Add("NotFoundException", "Documetn-Not-Found");
                context.ExceptionHandled = true;
            }
        }
    }
}

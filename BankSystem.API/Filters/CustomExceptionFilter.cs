using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace BankSystem.API.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ObjectNotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                context.Response.Content = new StringContent(context.Exception.Message);
            }
        }
    }
}
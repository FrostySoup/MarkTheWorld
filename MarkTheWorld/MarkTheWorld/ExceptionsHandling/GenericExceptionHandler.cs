using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace MarkTheWorld.ExceptionsHandling
{
    public class GenericExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new PlainTextErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "Unexpected error occured. Please see logs for more details.",
                Exr = context.Exception
            };
        }

        private class PlainTextErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }
            public string Content { get; set; }
            public Exception Exr { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<Error>(new Error
                    {
                        Message = Content,
                        StackTrace = Request.Content.ToString()
                    }, new BrowserJsonFormatter()), //new StringContent(Content), //
                    RequestMessage = Request
                };
                return Task.FromResult(response);
            }
        }
    }
    public class Error
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
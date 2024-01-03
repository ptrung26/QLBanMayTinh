using BTLWEB.Controllers.Exceptions;
using BTLWEB.Models.API;

namespace BTLWEB.Controllers.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.Response.ContentType = "application/json";
            if (exception is NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(
                    text: new ActionResultService()
                    {
                        StatusCode = StatusHttpCode.NotFound,
                        Message = exception.Message,
                        Success = false
                    }.ToString() ?? ""
                );
            }
            else if (exception is ConflictException conflictException)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(
                    text: new ActionResultService()
                    {
                        StatusCode = StatusHttpCode.Conflict,
                        Message = exception.Message,
                        Success = false
                    }.ToString() ?? ""
                );
            }
            else if (exception is BadRequestException badRequestException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(
                    text: new ActionResultService()
                    {
                        StatusCode = StatusHttpCode.BadRequest,
                        Message = exception.Message,
                        Success = false
                    }.ToString() ?? ""
                );
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(
                   text: new ActionResultService()
                   {
                       StatusCode = StatusHttpCode.ServerError,
                       Message = exception.Message,
                       Success = false
                   }.ToString() ?? ""
               );
            }
        }
    }
}

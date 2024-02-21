using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NZWALKS.Exceptions;

public class GlobalHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _requestDelegate;

    public GlobalHandler(ILogger<ExceptionHandlerMiddleware> logger,RequestDelegate requestDelegate )
    {
        _logger = logger;
        _requestDelegate = requestDelegate;
    }

    public async  Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception e)
        {
            var errorID = Guid.NewGuid;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var erromsg = new
            {
                Id = errorID,
                ErroMessage = "Something Went Wrong"
            };
            await context.Response.WriteAsJsonAsync(erromsg);

        }
    }
}
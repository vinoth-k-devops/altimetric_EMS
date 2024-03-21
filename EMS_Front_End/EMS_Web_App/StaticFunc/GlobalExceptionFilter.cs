﻿using System;
using System.Net;
using EMS_Web_App.Models;

namespace EMS_Web_App.StaticFunc
{
	public class GlobalExceptionFilter
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public GlobalExceptionFilter(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {

                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."

            }.ToString());
        }
    }
}


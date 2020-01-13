// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Net;
using System.Threading.Tasks;
using FileStorage.Implementation.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FileStorage.WebApi
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = (int)HttpStatusCode.InternalServerError; // 500 if unexpected
            var message = ex.Message;
            switch (ex)
            {
                case ArgumentNullException _:
                case ArgumentOutOfRangeException _:
                case ArgumentException _:
                    code = (int)HttpStatusCode.BadRequest;
                    break;

                case UserNotFoundException _:
                case FolderNotFoundException _:
                case FileNotFoundException _:
                    code = (int)HttpStatusCode.NotFound;
                    break;
            }

            _logger?.LogError(ex, message);

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(message);
        }
    }
}
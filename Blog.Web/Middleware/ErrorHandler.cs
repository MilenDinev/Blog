﻿namespace Blog.Web.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blog.Web.Models;

    public class ErrorHandler
    {
        private readonly RequestDelegate next;

        public ErrorHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    ArgumentException => (int)HttpStatusCode.Conflict,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    NullReferenceException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                var result = JsonSerializer.Serialize(new ErrorViewModel { RequestId = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}

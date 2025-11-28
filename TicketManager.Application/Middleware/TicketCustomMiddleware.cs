using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TicketManager.Application.DTO.RequestResponse;

namespace TicketManager.Application.Middleware
{
    public sealed class TicketCustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TicketCustomMiddleware> _logger;

        public TicketCustomMiddleware(RequestDelegate next, ILogger<TicketCustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {            
            Console.WriteLine("Processing request for: " + context.Request.Path);
            try
            {
                await _next(context);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = ex switch
                {
                    ApplicationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(new ResponseDTO {
                    IsValid = false,
                    Message = "An error occurred",
                    ResultData = ex.Message + " " + ex.StackTrace
                }));                
            }
        }
    }
}

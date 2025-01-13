using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace POS.AzureFunctionApp
{
    public class GlobalExceptionMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                // Proceed to the next middleware or function execution
                await next(context);
            }
            catch (Exception ex)
            {
                // Handle any unhandled exceptions
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(FunctionContext context, Exception ex)
        {
            // Log the exception (you can extend this to use Azure Application Insights or other logging services)
            Console.WriteLine($"Exception caught in middleware: {ex.Message}");

            // Create an error response
            var httpRequestData = await context.GetHttpRequestDataAsync();
            if (httpRequestData == null)
            {
                throw new InvalidOperationException("HttpRequestData is null");
            }
            var response = httpRequestData.CreateResponse();
            response.StatusCode = HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Unexpected error occurred. Please try again later."
            };

            response.Headers.Add("Content-Type", "application/json");
            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            await response.WriteStringAsync(jsonResponse);

            // Replace the context response with the newly created error response
            context.Features.Set(response);
        }
    }
}

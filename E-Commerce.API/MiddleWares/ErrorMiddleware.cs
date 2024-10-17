using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.API.MiddleWares
{
	public class ErrorMiddleware
	{
		readonly RequestDelegate _next;
		readonly ILogger<ErrorMiddleware> _logger;
		public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{

			try
			{
				await _next(context);
				// if next done there is 2 scenarios
				// => 1. successful response
				// => 2. not found end point
				//
				// make sure that error occur
				if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
					await NotFoundEndPointHandleAsync(context);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error Massage => {ex}");
				await ErrorHandleAsync(context, ex);
			}
		}

		private async Task NotFoundEndPointHandleAsync(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			var ErrorModel = new ErrorDetails()
			{
				StatusCode = (int)HttpStatusCode.NotFound,
				ErrorMessage = $"Not Found End Point With Path {context.Request.Path}"
			}.ToString();
			await context.Response.WriteAsync(ErrorModel);
		}

		private async Task ErrorHandleAsync(HttpContext context, Exception ex)
		{
			// Set Status Code 
			context.Response.StatusCode = ex switch
			{
				NotFoundException => (int)HttpStatusCode.NotFound,
				UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
				_=> (int)HttpStatusCode.InternalServerError // Default
			};
			// Set Content Type
			context.Response.ContentType = "application/json";
			var ErrorModel = new ErrorDetails()
			{
				StatusCode = (int)HttpStatusCode.InternalServerError,
				ErrorMessage = $"Error Massage => {ex.Message}"
			}.ToString();
			await context.Response.WriteAsync(ErrorModel);
		}
	}
}

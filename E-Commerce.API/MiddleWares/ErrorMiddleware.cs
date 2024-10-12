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
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error Massage => {ex}");
				await ErrorHandleAsync(context, ex);
			}
		}

		private async Task ErrorHandleAsync(HttpContext context, Exception ex)
		{
			// Set Status Code 
			context.Response.StatusCode = ex switch
			{
				NotFoundException => (int)HttpStatusCode.NotFound,
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

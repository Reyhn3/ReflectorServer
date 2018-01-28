using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Serilog;


namespace SimpleServer.Server
{
	public class Server
	{
		private readonly ILogger _logger;

		public Server(ILogger logger, IAppBuilder appBuilder)
		{
			_logger = logger.ForContext<Server>();
			appBuilder.Run(ReflectRequest);
		}

		private async Task ReflectRequest(IOwinContext context)
		{
			_logger.Information("Request received! {Url}", context.Request.Uri);

			var response = GenerateResponse(context.Request);
			context.Response.StatusCode = (int)HttpStatusCode.OK;
			await context.Response.WriteAsync(await response);
		}

		private static async Task<string> GenerateResponse(IOwinRequest request)
		{
			var sb = new StringBuilder();

			var actualBody = await ReadBodyAsString(request.Body);
			var body = string.IsNullOrWhiteSpace(actualBody) ? "N/A" : actualBody;

			sb.AppendLine($"RECEIVED: {DateTime.UtcNow}");
			sb.AppendLine($"METHOD: {request.Method}");
			sb.AppendLine($"BODY: {body}");

			return sb.ToString();
		}

		private static async Task<string> ReadBodyAsString(Stream stream)
		{
			if (stream == null || stream.Length == 0)
				return null;

			using (var reader = new StreamReader(stream))
			{
				var body = await reader.ReadToEndAsync();
				return body;
			}
		}
	}
}
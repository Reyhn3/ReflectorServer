using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;


namespace SimpleServer.Server
{
	public class Server
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			var config = new HttpConfiguration();

			//config.Formatters.Clear();
			//config.Formatters.Add(new JsonMediaTypeFormatter());

			appBuilder.Run(ReflectRequest);
		}

		private async Task ReflectRequest(IOwinContext context)
		{
			var response = GenerateResponse(context.Request);
			context.Response.StatusCode = (int)HttpStatusCode.OK;
			await context.Response.WriteAsync(await response);
		}

		private async Task<string> GenerateResponse(IOwinRequest request)
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
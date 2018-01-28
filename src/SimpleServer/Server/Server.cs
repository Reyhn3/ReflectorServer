using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Serilog;


namespace SimpleServer.Server
{
	internal class Server
	{
		private readonly ILogger _logger;
		private readonly Reflector _reflector;

		public Server(ILogger logger, IAppBuilder appBuilder, Reflector reflector)
		{
			_reflector = reflector;
			_logger = logger.ForContext<Server>();
			appBuilder.Run(ReflectRequest);
		}

		private async Task ReflectRequest(IOwinContext context)
		{
			_logger.Information("Request received! {Url} {Method}", context.Request.Uri, context.Request.Method);

			var response = _reflector.Generate(context.Request);
			context.Response.StatusCode = (int)HttpStatusCode.OK;
			await context.Response.WriteAsync(await response);
		}
	}
}
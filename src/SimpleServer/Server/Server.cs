using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using Serilog;


namespace SimpleServer.Server
{
	internal class Server
	{
		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Include
			};

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
			_logger.Debug("Request received on {Url} as {Method}", context.Request.Uri, context.Request.Method);

			var reflection = await _reflector.Generate(context.Request);
			var response = JsonConvert.SerializeObject(reflection, Formatting.Indented, _serializerSettings);
			await context.Response.WriteAsync(response);
			context.Response.StatusCode = (int)HttpStatusCode.OK;

			_logger.Information("{Method} {@sRequest}", context.Request.Method, reflection);
		}
	}
}
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;


namespace SimpleServer.Server
{
	internal class Reflector
	{
		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Include,
				ContractResolver = new RequestContractResolver()
			};

		public async Task<string> Generate(IOwinRequest request)
		{
			var body = await ReadBodyAsString(request.Body);
			var reflection = Reflection.FromRequest(request, body);
			var response = JsonConvert.SerializeObject(reflection, Formatting.Indented, _serializerSettings);
			return response;
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
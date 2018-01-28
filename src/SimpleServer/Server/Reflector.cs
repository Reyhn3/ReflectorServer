using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;


namespace SimpleServer.Server
{
	internal class Reflector
	{
		public async Task<Reflection> Generate(IOwinRequest request)
		{
			var body = await ReadBodyAsString(request.Body);
			var reflection = Reflection.FromRequest(request, body);
			return reflection;
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
using System;
using Microsoft.Owin;


namespace SimpleServer.Server
{
	internal class Reflection
	{
		private Reflection(IOwinRequest request, string body)
		{
			ReceivedUtc = DateTime.UtcNow;
			Body = body;
			Request = request;
		}

		public DateTime ReceivedUtc { get; }
		public string Body { get; }
		public IOwinRequest Request { get; }

		public static Reflection FromRequest(IOwinRequest request, string body)
		{
			return new Reflection(request, body);
		}
	}
}
using System;


namespace SimpleServer.Server
{
	internal class Reflection
	{
		public Reflection(string method, string body)
		{
			DateTimeUtc = DateTime.UtcNow;
			Method = method;
			Body = body;
		}

		public DateTime DateTimeUtc { get; }
		public string Method { get; }
		public string Body { get; }
	}
}
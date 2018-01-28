using System;
using System.Security.Principal;
using Microsoft.Owin;


namespace SimpleServer.Server
{
	internal class Reflection
	{
		private Reflection(string body)
		{
			ReceivedUtc = DateTime.UtcNow;
			Body = body;
		}

		public DateTime ReceivedUtc { get; }
		public string Body { get; }

		#region IOwinRequest Properties.
		// These properties are copied from the IOwinRequest.
		// This is done to allow destructuring without stack overflows.

		public string Method { get; private set; }
		public string Scheme { get; private set; }
		public bool IsSecure { get; private set; }
		public HostString Host { get; private set; }
		public PathString PathBase { get; private set; }
		public PathString Path { get; private set; }
		public QueryString QueryString { get; private set; }
		public IReadableStringCollection Query { get; private set; }
		public Uri Uri { get; private set; }
		public string Protocol { get; private set; }
		public IHeaderDictionary Headers { get; private set; }
		public RequestCookieCollection Cookies { get; private set; }
		public string ContentType { get; private set; }
		public string CacheControl { get; private set; }
		public string MediaType { get; private set; }
		public string Accept { get; private set; }
		public string LocalIpAddress { get; private set; }
		public int? LocalPort { get; private set; }
		public string RemoteIpAddress { get; private set; }
		public int? RemotePort { get; private set; }
		public IPrincipal User { get; private set; }
		#endregion IOwinRequest Properties.

		public static Reflection FromRequest(IOwinRequest request, string body)
		{
			return new Reflection(body)
				{
					Method = request.Method,
					Scheme = request.Scheme,
					IsSecure = request.IsSecure,
					Host = request.Host,
					PathBase = request.PathBase,
					Path = request.Path,
					QueryString = request.QueryString,
					Query = request.Query,
					Uri = request.Uri,
					Protocol = request.Protocol,
					Headers = request.Headers,
					Cookies = request.Cookies,
					ContentType = request.ContentType,
					CacheControl = request.CacheControl,
					MediaType = request.MediaType,
					Accept = request.Accept,
					LocalIpAddress = request.LocalIpAddress,
					LocalPort = request.LocalPort,
					RemoteIpAddress = request.RemoteIpAddress,
					RemotePort = request.RemotePort,
					User = request.User
				};
		}
	}
}
using System;
using Microsoft.Owin.Hosting;


namespace SimpleServer.Server
{
	public class Host : IDisposable
	{
		private readonly string _address;
		private IDisposable _server;

		public Host(string address)
		{
			_address = address;
		}

		public void Start()
		{
			_server = WebApp.Start<Server>(_address);
		}

		public void Stop()
		{
			Console.WriteLine("Stopping server.");
			_server?.Dispose();
		}

		public void Dispose()
		{
			Stop();
		}
	}
}
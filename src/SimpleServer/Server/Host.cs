using System;
using Microsoft.Owin.Hosting;
using Serilog;


namespace SimpleServer.Server
{
	public class Host : IDisposable
	{
		private readonly ILogger _logger;
		private readonly string _address;

		private IDisposable _server;

		public Host(ILogger logger, string address)
		{
			_logger = logger.ForContext<Host>();
			_address = address;
		}

		public void Start()
		{
			_logger.Debug("Starting server on {Address}.", _address);
			_server = WebApp.Start(_address, appBuilder => new Server(_logger, appBuilder));
		}

		public void Stop()
		{
			_logger.Debug("Stopping server.");
			_server?.Dispose();
		}

		public void Dispose()
		{
			Stop();
		}
	}
}
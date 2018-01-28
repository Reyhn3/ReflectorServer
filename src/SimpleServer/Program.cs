﻿using System;
using System.Reflection;
using System.Threading;
using clipr;
using clipr.Usage;
using Serilog;
using Serilog.Events;


namespace SimpleServer
{
	internal class Program
	{
		private static readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
		private static Host _host;

		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
			Console.CancelKeyPress += OnShutdown;

			Setup();
			Greet();
			var options = Configure(args);
			_host = Start(options);

			_resetEvent.WaitOne();
			Exit();
		}

		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("A fatal error occurred!");
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(e?.ExceptionObject?.ToString());
			Console.ResetColor();
			Console.WriteLine("The application will now exit.");
			Environment.Exit(1);
		}

		private static void OnShutdown(object sender, ConsoleCancelEventArgs e)
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Application termination detected.");
			e.Cancel = true;
			_resetEvent.Set();
		}

		private static void Setup()
		{
			var loggerConfiguration = new LoggerConfiguration()
				.MinimumLevel.Is(LogEventLevel.Verbose)
				.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff}] [{Level:u3}] {Message}{NewLine}");
			var logger = loggerConfiguration.CreateLogger();
			Log.Logger = logger;
		}

		private static void Greet()
		{
			var title = typeof(Program).Assembly.GetAttribute<AssemblyTitleAttribute>()?.Title ?? "SimpleServer";
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(title);
			Console.ResetColor();
			Console.WriteLine();
		}

		private static Options Configure(string[] args)
		{
			var options = CliParser.StrictParse<Options>(args);
			var parser = new CliParser<Options>(options);
			var help = new AutomaticHelpGenerator<Options>();

			var usage = help.GetUsage(parser.Config);
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine(usage);
			Console.ResetColor();
			Console.WriteLine();

			var current = help.GetValues(options);
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(current);
			Console.ResetColor();
			Console.WriteLine();

			return options;
		}
		
		private static Host Start(Options options)
		{
			var address = $"http://localhost:{options.Port}/";
			var host = new Host(address);
			host.Start();
			return host;
		}

		private static void Exit()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Shutting down.");
			Console.ResetColor();
		}


	}
}
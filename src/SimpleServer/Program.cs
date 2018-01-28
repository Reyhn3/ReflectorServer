using System;
using System.Reflection;
using System.Threading;
using clipr;
using clipr.Usage;


namespace SimpleServer
{
	internal class Program
	{
		private static readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
			Console.CancelKeyPress += OnShutdown;

			Greet();
			Configure(args);

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
			Console.WriteLine("Application termination detected.");
			e.Cancel = true;
			_resetEvent.Set();
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

			return options;
		}

		private static void Exit()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Shutting down.");
			Console.ResetColor();
		}
	}
}
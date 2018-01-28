using System;
using System.Reflection;
using clipr;
using clipr.Usage;


namespace SimpleServer
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

			Greet();
			Configure(args);


			Console.WriteLine();
			Console.WriteLine("Reflector Server started. Waiting for requests.");
			Console.ReadLine();
		}

		private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("A fatal error occurred!");
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(unhandledExceptionEventArgs?.ExceptionObject?.ToString());
			Console.ResetColor();
			Console.WriteLine("The application will now exit.");
			Environment.Exit(1);
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
	}
}
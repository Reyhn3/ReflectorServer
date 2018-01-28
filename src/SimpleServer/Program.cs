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
			Greet();
			Configure(args);


			Console.WriteLine();
			Console.WriteLine("Reflector Server started. Waiting for requests.");
			Console.ReadLine();
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
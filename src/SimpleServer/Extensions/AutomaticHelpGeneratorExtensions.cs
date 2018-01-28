using System.Linq;
using System.Text;
using clipr.Core;
using clipr.Usage;


namespace SimpleServer
{
	internal static class AutomaticHelpGeneratorExtensions
	{
		public static string GetValues<TOptions>(this AutomaticHelpGenerator<TOptions> help, TOptions settings)
		{
			var sb = new StringBuilder();

			sb.AppendLine("Current settings:");

			var properties = typeof(TOptions).GetProperties();
			var options = properties.Where(p => p.IsDefined(typeof(ArgumentAttribute), true));

			foreach (var option in options)
			{
				var value = option.GetValue(settings);
				sb.AppendLine($" {option.Name}: {value}");
			}

			return sb.ToString();
		}
	}
}
using clipr;


namespace SimpleServer
{
	[ApplicationInfo(Description = "A simple web server that listens for incoming requests and reflects them back to the response.")]
	public class Options
	{
		public Options()
		{
			Port = 9000;
		}

		[NamedArgument(
			'v',
			"verbose",
			Action = ParseAction.StoreFalse,
			Description = "Use verbose output.")]
		public bool Verbose { get; set; }

		[NamedArgument(
			'p',
			"port",
			Action = ParseAction.Store,
			Description = "The port to listen to.")]
		public ushort Port { get; set; }
	}
}
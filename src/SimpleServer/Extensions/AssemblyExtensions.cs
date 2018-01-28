using System;
using System.Reflection;


namespace SimpleServer
{
	internal static class AssemblyExtensions
	{
		public static TAttribute GetAttribute<TAttribute>(this Assembly assembly)
			where TAttribute : Attribute
		{
			if (assembly == null)
				return null;

			var attributes = assembly.GetCustomAttributes(typeof(TAttribute), false);
			if (attributes.Length == 0)
				return null;

			return (TAttribute)attributes[0];
		}
	}
}
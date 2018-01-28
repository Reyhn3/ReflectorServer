using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace SimpleServer.Server
{
	internal class RequestContractResolver : DefaultContractResolver
	{
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			var properties = base.CreateProperties(type, memberSerialization);
			var filtered = properties.Where(IsFriendlyProperty);
			return filtered.ToList();
		}

		private bool IsFriendlyProperty(JsonProperty property)
		{
			if (!typeof(IOwinRequest).IsAssignableFrom(property.DeclaringType))
				return true;

			switch (property.PropertyName)
			{
				case nameof(IOwinRequest.Accept):
				case nameof(IOwinRequest.CacheControl):
				case nameof(IOwinRequest.ContentType):
				case nameof(IOwinRequest.Cookies):
				case nameof(IOwinRequest.Headers):
				case nameof(IOwinRequest.Host):
				case nameof(IOwinRequest.IsSecure):
				case nameof(IOwinRequest.LocalIpAddress):
				case nameof(IOwinRequest.LocalPort):
				case nameof(IOwinRequest.MediaType):
				case nameof(IOwinRequest.Method):
				case nameof(IOwinRequest.Path):
				case nameof(IOwinRequest.PathBase):
				case nameof(IOwinRequest.Protocol):
				case nameof(IOwinRequest.Query):
				case nameof(IOwinRequest.QueryString):
				case nameof(IOwinRequest.RemoteIpAddress):
				case nameof(IOwinRequest.RemotePort):
				case nameof(IOwinRequest.Scheme):
				case nameof(IOwinRequest.User):
				case nameof(IOwinRequest.Uri):
					return true;
				default:
					return false;
			}
		}
	}
}
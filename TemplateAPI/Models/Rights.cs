using System.Text.Json.Serialization;

namespace TemplateAPI.Models
{
	// just a flags enum, to show how to serialise them with the JsonStringEnumConverter...


	[Flags]
	[JsonConverter(typeof(JsonStringEnumConverter))] // needed to serialise to JSON properly
	public enum Rights
	{
		CanLogin = 1,
		CanFeedTheCat = 2,
		CanOperateCementMixer = 4,
		CanChangeTheMusic = 8,
		CanOpenTheFridge = 16
	}
}

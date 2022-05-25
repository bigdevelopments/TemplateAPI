using System.Text.Json.Serialization;

namespace TemplateAPI.Models
{
	// just a flags enum, to show how to serialise them with the JsonStringEnumConverter...


	[Flags]
	[JsonConverter(typeof(JsonStringEnumConverter))] // needed to serialise to JSON properly
	public enum Traits
	{
		Reasonable = 1,
		GoodInAFight = 2,
		Eccentric = 4,
		Duplicitous = 8,
		ThickAsMince = 16
	}
}

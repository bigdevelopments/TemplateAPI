using TemplateAPI.SerialisationConverters;
using System.Text.Json.Serialization;

namespace TemplateAPI.Models
{
	public class Robot
	{
		public int Version { get; set; }
		public string Name { get; set; }

		public string WakeWord { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))] // needed else enums go out as numbers
		public Traits Traits { get; set; }

		public DateTime LastStarted { get; set; }

		[JsonConverter(typeof(DateTimeAsDateConverter))] // when converting to json, use the yyyy-mm-dd format...
		public DateTime DateOfManufacture { get; set; }

		public Robot(string name)
		{
			Version = 101;
			Name = name;
			WakeWord = "Clondyke";
			Traits = Traits.GoodInAFight | Traits.Duplicitous;
			LastStarted = DateTime.Now;
			DateOfManufacture = new DateTime(1991, 9, 1);
		}
	}
}

using TemplateAPI.SerialisationConverters;
using System.Text.Json.Serialization;

namespace TemplateAPI.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string Password { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))] // needed else enums go out as numbers
		public Rights Rights { get; set; }

		public DateTime Timestamp { get; set; }

		[JsonConverter(typeof(DateTimeAsDateConverter))] // when converting to json, use the yyyy-mm-dd format...
		public DateTime DateOfBirth { get; set; }

		public User(string name)
		{
			Id = 101;
			Name = name;
			Password = "ThatsNotMyBucket";
			Rights = Rights.CanFeedTheCat | Rights.CanOperateCementMixer;
			Timestamp = DateTime.Now;
			DateOfBirth = new DateTime(1991, 9, 1);
		}
	}
}

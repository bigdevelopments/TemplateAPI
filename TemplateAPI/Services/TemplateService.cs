namespace TemplateAPI.Services
{
	// nothing going on here at all, just a really stupid 'service' for injection
	// and calling from endpoints

	public interface ITemplateService
	{
		string GetATextString();
		string GetAJsonString();
	}

	public class TemplateService : ITemplateService
	{ 
		public string GetATextString()
		{
			// just a bit of text
			return "Hello ugly";
		}

		public string GetAJsonString()
		{
			// a bit of text which is also valid json
			return "{\"age\":50}";
		}
	}
}

using TemplateAPI.Models;
using TemplateAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// *** dependency injection bootstrap ***
builder.Services.AddSingleton<ITemplateService, TemplateService>();

// construct the app
var app = builder.Build();

// *** routing ***

// simple hello world endpoint (content-type = text/plain; charset=utf-8)
app.Map("/helloworld", () => "Hello World");

// calling into a function to provide the output (function uses a bit of DI, content-type = text/plain; charset=utf-8)
app.Map("/helloworld2", HelloWorld2);

object HelloWorld2(ITemplateService service)
{
    return service.GetATextString();
}

// returning JSON instead (with correct content-type), using a DI lamba..
app.MapGet("/", (ITemplateService service) =>
    Results.Content(service.GetAJsonString(), "application/json; charset=utf-8"));

// return JSON, this time just an object which gets serilalized for us, using URL parameter as the name
app.MapGet("/user/{name}", (string name) =>
{
    return new User(name);
});

// *** run ***
app.Run();

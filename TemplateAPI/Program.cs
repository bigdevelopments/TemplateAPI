// let's go...
var builder = WebApplication.CreateBuilder(args);

// *** dependency injection bootstrap ***

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITemplateService, TemplateService>();

// construct the app
var app = builder.Build();

// *** middleware ***

if (app.Environment.IsDevelopment())
{ 
	// for development builds use swagger
	app.UseSwagger();
	app.UseSwaggerUI();
}

// we also need to authenticate and authorise 
app.UseAuthentication();
app.UseAuthorization();

// *** routing ***

// simple anonymous hello world endpoint (content-type = text/plain; charset=utf-8)
app.MapGet("/helloworld", () => "Hello World");

// calling into a function to provide the output (function uses a bit of DI, content-type = text/plain; charset=utf-8)
app.MapGet("/helloworld2", HelloWorld2);

// redirect example
app.Map("/helloworld3", () => Results.Redirect("helloworld2"));

// returning JSON instead (with correct content-type), using a DI lamba..
app.MapGet("/", (ITemplateService service) =>
	Results.Content(service.GetAJsonString(), "application/json; charset=utf-8"));

// return JSON, this time just an object which gets serilalized for us, using URL parameter as the name
app.MapGet("/robot/{name}", (string name) =>
{
	return new Robot(name);
});

// secured endpoint, JWT must claim to be in the admin role
app.MapGet("/secret", [Authorize(Roles = "admin")] (HttpContext context) => Results.Json("secured info"));

// *** run ***
app.Run();



static IResult HelloWorld2(ITemplateService service)
{
	return Results.Json(service.GetATextString());
}
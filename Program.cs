using Coravel;
using upbeat.Tasks;

var builder = WebApplication.CreateBuilder(args);
// configuration goes here
builder.Services.AddTransient<CheckForString>();
builder.Services.AddScheduler();

var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<CheckForString>().Hourly();

});

app.Urls.Add("http://localhost:5002");

app.MapGet("/", () => "Hello World!");

app.Run();
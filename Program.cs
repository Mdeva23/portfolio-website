using PortfolioApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Resolve wwwroot whether running locally or published
var wwwrootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot");
if (!Directory.Exists(wwwrootPath))
    wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

builder.Environment.WebRootPath = wwwrootPath;

var app = builder.Build();

app.UseCors();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToFile("index.html");

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
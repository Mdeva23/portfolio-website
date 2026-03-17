using PortfolioApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Resolve wwwroot whether running locally or published
var wwwrootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot");
if (!Directory.Exists(wwwrootPath))
    wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

builder.Environment.WebRootPath = wwwrootPath;

var app = builder.Build();

// Enable CORS
app.UseCors();

// Use HTTPS only in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Serve static files
app.UseDefaultFiles();
app.UseStaticFiles();

// Map controllers
app.MapControllers();

// Fallback to index.html for SPA
app.MapFallbackToFile("index.html");

// Get port from environment (Render) or default to 5000
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

// Run app differently for dev vs production
if (app.Environment.IsDevelopment())
{
    // Local development uses localhost
    app.Run($"http://localhost:{port}");
}
else
{
    // Production (Render) uses 0.0.0.0
    app.Run($"http://0.0.0.0:{port}");
}
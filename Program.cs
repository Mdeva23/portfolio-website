using PortfolioApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Configure CORS (allow all origins, headers, methods)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Resolve wwwroot whether running locally or in Render
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

// Fallback to index.html for SPA routing
app.MapFallbackToFile("index.html");

// Get port from environment Render or default to 5000
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

// Run app
if (app.Environment.IsDevelopment())
{
    // Local development
    app.Run($"http://localhost:{port}");
}
else
{
    // Render
    app.Run($"http://0.0.0.0:{port}");
}
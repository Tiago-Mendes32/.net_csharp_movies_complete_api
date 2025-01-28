using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesAPI.Data;
using System.Reflection;

// Create a builder for configuring the web application.
var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core to use MySQL as the database provider.
// The connection string is retrieved from the application's configuration.
builder.Services.AddDbContext<MovieContext>(opts =>
    opts.UseLazyLoadingProxies().UseMySql(
        builder.Configuration.GetConnectionString("MovieConnection"), // Get the connection string for the database.
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MovieConnection")) // Automatically detect the MySQL server version.
    ));

// Add services to the dependency injection container.

// Add controller support with Newtonsoft.Json for advanced JSON serialization options.
builder.Services.AddControllers().AddNewtonsoftJson();

// Register AutoMapper and scan the current domain's assemblies for mapping profiles.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add support for endpoint API explorer (used for Swagger).
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger for API documentation.
builder.Services.AddSwaggerGen(c =>
{
    // Define the OpenAPI documentation version and title.
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoviesAPI", Version = "v1" });

    // Include XML comments in the Swagger documentation.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Get the XML file name.
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Determine the file path.
    c.IncludeXmlComments(xmlPath); // Add the XML comments to Swagger.
});

// Build the application.
var app = builder.Build();

// Configure middleware and the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger and Swagger UI in the development environment.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS redirection for all requests.
app.UseHttpsRedirection();

// Enable the default authorization middleware.
app.UseAuthorization();

// Map controller routes to handle HTTP requests.
app.MapControllers();

// Run the application.
app.Run();
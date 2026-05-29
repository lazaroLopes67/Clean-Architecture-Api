using Criando_Minha_Primeira_API.Context;
using Criando_Minha_Primeira_API.DTOs.Mapping;
using Criando_Minha_Primeira_API.Extension;
using Criando_Minha_Primeira_API.Logging;
using Criando_Minha_Primeira_API.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions
    .ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Retrieves the database connection string from configuration
string? SQLstringConnection = builder.Configuration["ConnectionStrings:DefaultConnection"];

// Registers the application's database context and configures the MySQL connection.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(SQLstringConnection, ServerVersion.AutoDetect(SQLstringConnection));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Logging.AddProvider(new LoggingProvider(builder.Configuration));
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ConfigureExceptionsHandler();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "api");
    });
}
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

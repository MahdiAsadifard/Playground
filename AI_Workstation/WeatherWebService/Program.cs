using WeatherWebService.Models;
using WeatherWebService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Configure<OpenWeatherMapOptions>(
    builder.Configuration.GetSection(OpenWeatherMapOptions.SectionName));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4444")
            .WithMethods("GET")
            .AllowAnyHeader();
    });
});
builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "WeatherWebService API");
    });
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

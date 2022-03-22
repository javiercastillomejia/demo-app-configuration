using ApiDemoAppConfigurationCommon.Configuration;
using ApiDemoAppConfigurationNet60.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host
.AzureAppConfigurationBuild()
.ConfigureServices(services => services.AddControllersWithViews());

builder.Services.Configure<Config>(builder.Configuration.GetSection("Config")); // Important
builder.Services.AddAzureAppConfiguration(); // Important

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var groupName = "v1";

    options.SwaggerDoc(groupName, new OpenApiInfo
    {
        Title = $"App configuration {groupName}",
        Version = groupName,
        Description = "Almacenamiento de parámetros rápido y escalable para la configuración de aplicaciones",
        Contact = new OpenApiContact
        {
            Name = "Javier Castillo Mejía",
            Email = string.Empty,
            Url = new Uri("https://github.com/javiercastillomejia"),
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "App configuration V1");
    });
}

app.UseAzureAppConfiguration(); // Important
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

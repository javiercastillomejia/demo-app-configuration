using ApiDemoAppConfigurationNet60.Configuration;
using ApiDemoAppConfigurationNet60.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppConfig"); // Important

builder.Host
.AzureAppConfigurationBuild(connectionString)
.ConfigureServices(services => services.AddControllersWithViews());

builder.Services.Configure<Config>(builder.Configuration.GetSection("Config")); // Important
builder.Services.AddAzureAppConfiguration(); // Important

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using ApiDemoAppConfiguration.Configuration;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppConfig");

builder.Host
.ConfigureAppConfiguration(builder => builder.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
           .ConfigureRefresh(refresh =>
            {
                refresh.Register("Config:Sentinel", refreshAll: true)
                       .SetCacheExpiration(new TimeSpan(0, 0, 5));
            });
}))
.ConfigureServices(services => services.AddControllersWithViews());


// Add services to the container.
builder.Services.Configure<Config>(builder.Configuration.GetSection("Config"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureAppConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

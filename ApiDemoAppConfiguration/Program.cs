using ApiDemoAppConfiguration.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppConfig"); // Important

builder.Host
.ConfigureAppConfiguration(builder => builder.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
           .ConfigureRefresh(refresh =>
            {
                refresh.Register("Config:Sentinel", refreshAll: true) // Important: It is for hot data refreshing.
                       .SetCacheExpiration(new TimeSpan(0, 0, 5));
            });
}))
.ConfigureServices(services => services.AddControllersWithViews()); // Important

builder.Services.Configure<Config>(builder.Configuration.GetSection("Config")); // Important
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureAppConfiguration(); // Important

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

using Base.API.Services.Measurements;
using Base.API.Services.Sensors;
using Base.Broker.Loggings;
using Base.Data;
using Base.Data.UnitOfWork;
using Base.Util.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true).Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<DataBaseConfiguration>(configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<BaseProjectContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddLogging();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
builder.Services.AddTransient<ISensorService, SensorService>();
builder.Services.AddTransient<IMeasurementService, MeasurementService>();

builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();
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
public partial class Program { }
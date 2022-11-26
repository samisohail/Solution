using Serilog;
using Serilog.Formatting.Json;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text.Json;
using System.Xml;
using Web.Setup;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services); // Add services to the container.

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production"}.json")
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

//builder.Services.AddLogging((loggingBuilder =>
//    loggingBuilder.AddSerilog(logger)));


builder.Host.UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
{
    // var serilogConfig = hostBuilderContext.Configuration.GetSection("Serilog");
    // loggerConfiguration.ReadFrom.Configuration(serilogConfig);

    var serilog = hostBuilderContext.Configuration["Serilog"];

    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.WriteTo.File("logs/log.json", rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

startup.Configure(app, builder.Environment);


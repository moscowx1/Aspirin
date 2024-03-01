using Autofac;
using Autofac.Extensions.DependencyInjection;
using Configurations;
using Microsoft.EntityFrameworkCore;
using Model;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule<AppModule>()
);

builder
    .Services.AddOptions<Configuration>()
    .BindConfiguration("")
    .ValidateFluentValidation()
    .ValidateOnStart();

var otel = builder.Services.AddOpenTelemetry();

otel.ConfigureResource(resource =>
    resource.AddService(serviceName: builder.Environment.ApplicationName)
);

otel.WithMetrics(metrics =>
    metrics
        .AddAspNetCoreInstrumentation()
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        .AddPrometheusExporter()
);

otel.WithTracing(tracing =>
{
    var endpoint = builder.Configuration[
        nameof(Configuration.ConnectionStrings) + ":" + nameof(ConnectionStrings.OltpEndpoint)
    ];

    Console.WriteLine($"Endpoint: '{endpoint}'");
    tracing.AddAspNetCoreInstrumentation();
    tracing.AddHttpClientInstrumentation();
    if (endpoint is not null)
    {
        tracing.AddOtlpExporter(otlpOptions => otlpOptions.Endpoint = new Uri(endpoint));
    }
});

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

var env = app.Environment.EnvironmentName;
logger.LogInformation("Starting app in {env}", env);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    logger.LogInformation("Migrating database");
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using var scope = scopeFactory.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Db>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

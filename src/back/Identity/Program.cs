using Autofac;
using Autofac.Extensions.DependencyInjection;
using Configurations;
using Microsoft.EntityFrameworkCore;
using Model;
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

using Autofac;
using Configurations;
using FluentValidation;
using Model;

public class AppModule(IConfiguration configuration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(Program).Assembly;

        builder
            .RegisterAssemblyTypes(assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterInstance<IConfiguration>(configuration).SingleInstance();

        builder.RegisterType<Db>().InstancePerLifetimeScope();

        //builder.RegisterType<M>().As<IHostedService>().SingleInstance();
    }
}

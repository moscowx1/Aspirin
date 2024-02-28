using Autofac;
using FluentValidation;
using Model;

public class AppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        var assembly = typeof(Program).Assembly;
        builder
            .RegisterAssemblyTypes(assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces();

        builder.RegisterType<Db>().InstancePerRequest();
    }
}

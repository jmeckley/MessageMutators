namespace Sample.Core.Initialization
{
    using Mehdime.Entity;
    using NServiceBus;
    using Sample.Core.Implementation;

    public class RegisterUnitOfWork
        : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(components =>
            {
                components.ConfigureComponent<DbContextFactory>(DependencyLifecycle.InstancePerCall);
                components.ConfigureComponent<DbContextScopeFactory>(DependencyLifecycle.InstancePerCall);
                components.ConfigureComponent<AmbientDbContextLocator>(DependencyLifecycle.InstancePerCall);

                components.ConfigureComponent<UnitOfWork>(DependencyLifecycle.InstancePerCall);
            });
        }
    }
}
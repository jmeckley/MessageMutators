namespace Sample.Core.Initialization
{
    using NServiceBus;
    using Sample.Core.Implementation;

    public class ShimInitialization 
        : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(components =>
            {
                components.ConfigureComponent<Shim>(DependencyLifecycle.InstancePerCall);
                components.ConfigureComponent<MappingEngine>(DependencyLifecycle.InstancePerCall);
            });
        }
    }
}
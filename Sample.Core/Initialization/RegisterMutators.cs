namespace Sample.Core.Initialization
{
    using NServiceBus;
    using Sample.Core.Implementation;

    public class RegisterMutators
        : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(components =>
            {
                components
                    .ConfigureComponent<IncludeMetadataMessageMutator>(DependencyLifecycle.InstancePerCall)
                    .ConfigureProperty(x => x.Source, "myCerts");
                components.ConfigureComponent<ValidationMessageMutator>(DependencyLifecycle.InstancePerCall);
                components.ConfigureComponent<TransportMessageCompressionMutator>(DependencyLifecycle.InstancePerCall);
                components.ConfigureComponent<ReadMetadataMessageMutator>(DependencyLifecycle.InstancePerCall);
            });
        }
    }
}
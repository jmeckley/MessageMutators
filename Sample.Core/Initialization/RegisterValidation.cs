namespace Sample.Core.Initialization
{
    using NServiceBus;
    using Sample.Core.Implementation;

    public class RegisterValidation
        : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(components =>
            {
                components.ConfigureComponent(typeof(GenericValidator<>), DependencyLifecycle.InstancePerCall);
                components.ConfigureComponent<CreateProductCommandValidator>(DependencyLifecycle.InstancePerCall);
                components.ConfigureComponent<CommonValidatorFactory>(DependencyLifecycle.InstancePerCall);
            });
        }
    }
}
namespace Sample
{
    using Mehdime.Entity;
    using NServiceBus;
    using NServiceBus.Logging;
    using Sample.Core.Commands;

    public class Handler
        : IHandleMessages<CreateProductCommand>
    {
        static ILog log = LogManager.GetLogger<Handler>();

        readonly IAmbientDbContextLocator _locator;

        public Handler(IAmbientDbContextLocator locator)
        {
            _locator = locator;
        }

        public void Handle(CreateProductCommand createProductCommand)
        {
            var x = _locator.Get<MyDbContext>();
            log.Info("Received a CreateProductCommand message: " + createProductCommand);
        }
    }
}
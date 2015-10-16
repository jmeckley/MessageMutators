namespace Sample
{
    using System;
    using Microsoft.Practices.Unity;
    using NServiceBus;
    using Sample.Core;
    using Sample.Core.Commands;
    using Sample.UnityConfiguration;

    class Program
    {
        static IUnityContainer _container;

        public static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => _container.Dispose();

            _container = new UnityContainer();
            ConfigureContainer();

            //needed to appease NSB. otherwise it will error with
            //System.Data.SqlClient.SqlException (0x80131904): CREATE DATABASE statement not a llowed within multi-statement transaction.
            GenerateDatabase();
            StartTheBus();

            _container.Resolve<Runner>().Run();
        }

        static void GenerateDatabase()
        {
            using (var context = _container.Resolve<MyDbContext>())
            using (var transaction = context.Database.BeginTransaction())
            {
                transaction.Commit();
            }
        }

        static void StartTheBus()
        {
            var configuration = new BusConfiguration();
            configuration.UseContainer<UnityBuilder>(c =>
            {
                _container.RegisterType<IContainerAdaptor, UnityContainerAdaptor>();
                c.UseExistingContainer(_container);
            });
            configuration.EndpointName("Samples.MessageMutators");
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();

            Bus.Create(configuration).Start();
        }

        static void ConfigureContainer()
        {
            //_container.RegisterTypes(AllClasses.FromLoadedAssemblies(), WithMappings.FromMatchingInterface, WithName.Default, WithLifetime.Transient);
            _container.RegisterInstance(Console.Out);
            _container.RegisterType<MyDbContext>();
            _container.RegisterType<Runner>();
            _container.RegisterType<IMapper<Product, CreateProductCommand>, ProductToCreateProductCommandMapper>();
        }
    }
}
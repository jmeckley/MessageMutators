namespace Sample.Core.Implementation
{
    using System.Data.Entity;
    using Mehdime.Entity;

    public class DbContextFactory 
        : IDbContextFactory
    {
        readonly IContainerAdaptor _container;

        public DbContextFactory(IContainerAdaptor container)
        {
            _container = container;
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return _container.Resolve<TDbContext>();
        }
    }
}
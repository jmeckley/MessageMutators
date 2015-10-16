using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Sample
{
    [DbConfigurationType(typeof(MyConfiguration))] 
    public class MyDbContext 
        : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public class MyConfiguration 
            : DbConfiguration
        {
            public MyConfiguration()
            {
                SetExecutionStrategy("System.Data.SqlClient", () => new DefaultExecutionStrategy());
                SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
            }
        } 
    }
}
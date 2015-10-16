namespace Sample.Core.Implementation
{
    using System;
    using System.Data;
    using Mehdime.Entity;
    using NServiceBus.UnitOfWork;
    
    public class UnitOfWork
        : IManageUnitsOfWork
    {
        readonly IDbContextScopeFactory _uow;
        IDbContextScope _scope;

        public UnitOfWork(IDbContextScopeFactory uow)
        {
            _uow = uow;
        }

        public void Begin()
        {
            _scope = _uow.CreateWithTransaction(IsolationLevel.Serializable);
        }

        public void End(Exception exception = null)
        {
            using (_scope)
            {
                if (exception != null) return;
                _scope.SaveChanges();
            }
        }
    }
}
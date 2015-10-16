namespace Sample.UnityConfiguration
{
    using System;
    using Microsoft.Practices.Unity;
    using Sample.Core;

    public class UnityContainerAdaptor 
        : IContainerAdaptor
    {
        readonly IUnityContainer _container;

        public UnityContainerAdaptor(IUnityContainer container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
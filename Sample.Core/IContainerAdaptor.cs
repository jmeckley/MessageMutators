namespace Sample.Core
{
    using System;

    public interface IContainerAdaptor
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}
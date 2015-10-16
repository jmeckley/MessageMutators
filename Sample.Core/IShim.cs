namespace Sample.Core
{
    using System;

    public interface IShim
    {
        void Execute<TInput, TCommand>(TInput input, Action<TInput> action);
    }
}
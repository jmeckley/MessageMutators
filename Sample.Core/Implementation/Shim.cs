namespace Sample.Core.Implementation
{
    using System;
    using FluentValidation;
    using NServiceBus;

    public class Shim
        : IShim
    {
        readonly IBus _bus;
        readonly IMappingEngine _mappingEngine;
        readonly IValidatorFactory _validatorFactory;

        public Shim(IBus bus, IMappingEngine mappingEngine, IValidatorFactory validatorFactory)
        {
            _bus = bus;
            _mappingEngine = mappingEngine;
            _validatorFactory = validatorFactory;
        }

        public void Execute<TInput, TCommand>(TInput input, Action<TInput> action)
        {
            var command = _mappingEngine.Map<TInput, TCommand>(input);
            var validation = _validatorFactory.GetValidator<TCommand>().Validate(command);

            if (validation.IsValid == false)
            {
                throw new ValidationException(validation.Errors);
            }

            action(input);

            _bus.SendLocal(command);
        }
    }
}

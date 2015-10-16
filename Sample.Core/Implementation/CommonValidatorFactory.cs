namespace Sample.Core.Implementation
{
    using System;
    using FluentValidation;

    /// <summary>
    /// Unity Validator Factory
    /// </summary>
    /// <remarks>This uses Fluent Validator. See http://www.jeremyskinner.co.uk/2010/02/22/using-fluentvalidation-with-an-ioc-container/
    /// for more information.</remarks>
    public class CommonValidatorFactory
        : ValidatorFactoryBase
    {
        private readonly IContainerAdaptor _container;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">Unity container</param>
        public CommonValidatorFactory(IContainerAdaptor container)
        {
            _container = container;
        }

        /// <summary>
        /// Create an instance of the validator
        /// </summary>
        /// <param name="validatorType">Validator type</param>
        /// <returns>Resolved validator or null if not found</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.Resolve(validatorType) as IValidator;
        }
    }
}


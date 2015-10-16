using System.Linq;
using System.Text;
using FluentValidation;
using NServiceBus.Logging;
using NServiceBus.MessageMutator;

namespace Sample.Core.Implementation
{
    public class ValidationMessageMutator
        : IMessageMutator
    {
        static ILog log = LogManager.GetLogger("ValidationMessageMutator");
        static IValidatorFactory _validatorFactory;

        public ValidationMessageMutator(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public object MutateOutgoing(object message)
        {
            Validate(message);
            return message;
        }

        public object MutateIncoming(object message)
        {
            Validate(message);
            return message;
        }

        static void Validate(object message)
        {
            var validationResult = _validatorFactory
                .GetValidator(message.GetType())
                .Validate(message);

            if (validationResult.IsValid)
            {
                log.Info("Validation succeeded for message: " + message);
                return;
            }

            var builder = new StringBuilder()
                .AppendFormat("Validation failed for message {0}, with the following error/s:", message)
                .AppendLine();
            var errorMessage = validationResult
                .Errors
                .Select(error => string.Format("Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage))
                .Aggregate(builder, (b, error) => b.AppendLine(error))
                .ToString();

            log.Error(errorMessage);

            throw new ValidationException(validationResult.Errors);
        }
    }
}
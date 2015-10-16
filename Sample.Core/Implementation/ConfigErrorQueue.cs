using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Sample.Core.Implementation
{
    public class ConfigErrorQueue 
        : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error"
            };
        }
    }
}
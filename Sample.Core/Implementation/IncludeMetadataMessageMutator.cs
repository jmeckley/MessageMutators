using NServiceBus;
using NServiceBus.MessageMutator;
using NServiceBus.Unicast.Messages;

namespace Sample.Core.Implementation
{
    public class IncludeMetadataMessageMutator
        : IMutateOutgoingTransportMessages
    {
        public string Source { get; set; }

        public void MutateOutgoing(LogicalMessage logicalMessage, TransportMessage transportMessage)
        {
            var headers = transportMessage.Headers;
            if (headers.ContainsKey(Constants.Headers.Keys.Originator)) return;

            headers.Add(Constants.Headers.Keys.Originator, Source);
        }
    }
}
using System;
using System.Collections.Generic;
using NServiceBus;
using NServiceBus.MessageMutator;

namespace Sample.Core.Implementation
{
    using NServiceBus.Logging;

    public class ReadMetadataMessageMutator
        : IMutateIncomingTransportMessages
    {
        static ILog _log = LogManager.GetLogger("TransportMessageCompressionMutator");

        readonly string _key;

        public ReadMetadataMessageMutator()
        {
            _key = Constants.Headers.Keys.Originator;
        }

        public void MutateIncoming(TransportMessage transportMessage)
        {
            var source = Source(transportMessage.Headers);

            _log.InfoFormat("{0}: {1}", _key, source);
        }

        private string Source(IDictionary<string, string> headers)
        {
            string source;
            if (headers.TryGetValue(_key, out source)) return source;

            var message = string.Format("Header '{0}' is missing from the transport message", Constants.Headers.Keys.Originator);
            throw new Exception(message);
        }
    }
}
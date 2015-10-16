using System.IO;
using System.IO.Compression;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.MessageMutator;
using NServiceBus.Unicast.Messages;

namespace Sample.Core.Implementation
{
    public class TransportMessageCompressionMutator 
        : IMutateTransportMessages
    {
        static ILog log = LogManager.GetLogger("TransportMessageCompressionMutator");

        public void MutateOutgoing(LogicalMessage message, TransportMessage transportMessage)
        {
            log.Info("transportMessage.Body size before compression: " + transportMessage.Body.Length);

            transportMessage.Body = Compress(transportMessage.Body);
            transportMessage.Headers.Add(Constants.Headers.Keys.Compressed, Constants.Headers.Values.Compressed);

            log.Info("transportMessage.Body size after compression: " + transportMessage.Body.Length);
        }

        private byte[] Compress(byte[] raw)
        {
            using (var mStream = new MemoryStream(raw))
            using (var outStream = new MemoryStream())
            using (var tinyStream = new GZipStream(outStream, CompressionMode.Compress))
            {
                mStream.CopyTo(tinyStream);
                tinyStream.Close();

                // copy the compressed buffer only after the GZipStream is disposed, otherwise, not all the compressed message will be copied.
                return outStream.ToArray();
            }
        }

        public void MutateIncoming(TransportMessage transportMessage)
        {
            if (transportMessage.Headers.ContainsKey(Constants.Headers.Keys.Compressed) == false) return;
            
            using (GZipStream bigStream = new GZipStream(new MemoryStream(transportMessage.Body), CompressionMode.Decompress))
            using (var bigStreamOut = new MemoryStream())
            {
                bigStream.CopyTo(bigStreamOut);
                transportMessage.Body = bigStreamOut.ToArray();
            }
        }
    }
}
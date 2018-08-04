namespace ODriveClientLibrary
{
    using System;
    using System.Threading;
    using ODriveClientLibrary.Utilities;

    internal class Request
    {
        public ushort EncodedSequenceNumber { get; private set; }
        public ushort SequenceNumber { get; private set; }
        public ushort EncodedEndpointID { get; private set; }
        public ushort EndpointID { get; private set; }
        public ushort ExpectedResponseSize { get; private set; }
        public bool RequestACK { get; private set; }
        public ushort Signature { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        public WireBuffer Body { get; private set; }
        public Action<Response> ResponseCallback { get; private set; }

        public Request(
            ushort sequenceNumber,
            ushort endpointID,
            ushort expectedResponseSize,
            bool requestACK,
            Func<WireBuffer> populateBody,
            Action<Response> responseCallback,
            ushort signature,
            CancellationToken cancellationToken)
        {
            // NOTE: Regarding the 0x80 value, see https://github.com/madcowswe/ODrive/blob/eb07260c3ea57e74c59432fd036b275b608d85d0/Firmware/fibre/python/fibre/protocol.py#L276
            SequenceNumber = sequenceNumber;
            EncodedSequenceNumber = (ushort)(sequenceNumber | 0x80);
            EndpointID = endpointID;
            EncodedEndpointID = (ushort)(requestACK ? endpointID | 0x8000 : endpointID);
            ExpectedResponseSize = expectedResponseSize;
            RequestACK = requestACK;
            Body = populateBody.Invoke();
            ResponseCallback = responseCallback;
            Signature = signature;
            CancellationToken = cancellationToken;

            if (Body == null)
            {
                Body = new WireBuffer(0);
            }
        }

        public byte[] ToByteArray()
        {
            var outputBuffer = new WireBuffer(Body.Data.Length + 8);

            // For general protocol doc this is written against, see https://github.com/madcowswe/ODrive/blob/1294ddff1dd0619e9f098ce12ca0936670a5b405/docs/protocol.md
            // Bytes 0, 1 Sequence number, MSB = 0
            // Bytes 2, 3 Endpoint ID
            // Bytes 4, 5 Expected response size
            // Bytes 6 to N-3 Payload
            // Bytes N-2, N-1 signature, depends on endpoint

            outputBuffer
                .Write(EncodedSequenceNumber)
                .Write(EncodedEndpointID)
                .Write(ExpectedResponseSize)
                .Write(Body.Data)
                .Write(Signature);

            return outputBuffer.Data;
        }
    }
}

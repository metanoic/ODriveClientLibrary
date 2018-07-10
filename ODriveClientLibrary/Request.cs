namespace ODrive
{
    using System;
    using ODrive.Utilities;

    internal class Request
    {
        private static readonly ThreadSafeCounter SequenceCounter = new ThreadSafeCounter(128);

        public ushort EndpointID { get; private set; }
        public ushort ExpectedResponseSize { get; private set; }
        public bool RequestACK { get; private set; }
        public ushort SequenceNumber { get; private set; }
        public ushort Signature { get; private set; }

        public WireBuffer Body { get; private set; }
        public Action<Request, Response> ResponseCallback { get; private set; }
        public bool CancellationRequested { get; private set; }

        public Request(
            ushort endpointID,
            ushort expectedResponseSize,
            bool requestACK,
            Func<WireBuffer> populateBody,
            Action<Request, Response> responseCallback,
            ushort signature)
        {
            EndpointID = endpointID;
            ExpectedResponseSize = expectedResponseSize;
            RequestACK = requestACK;
            Signature = signature;
            ResponseCallback = responseCallback;
            Body = populateBody.Invoke();

            if (Body == null)
            {
                Body = new WireBuffer(0);
            }

            // This should be the sole source of SequenceNumber values
            var seqNo = SequenceCounter.NextValue();
            SequenceNumber = (ushort)(seqNo | 0x80);
        }

        public void CancelRequest()
        {
            // Just set a flag we'll check later if we receive the response
            CancellationRequested = true;
        }

        public byte[] ToByteArray()
        {
            var outputBuffer = new WireBuffer(Body.Data.Length + 8);

            // NOTE: Regarding the 0x80 value, see https://github.com/madcowswe/ODrive/blob/eb07260c3ea57e74c59432fd036b275b608d85d0/Firmware/fibre/python/fibre/protocol.py#L276
            // For general protocol doc this is written against, see https://github.com/madcowswe/ODrive/blob/1294ddff1dd0619e9f098ce12ca0936670a5b405/docs/protocol.md
            ushort outgoingSequenceNumber = (ushort)(SequenceNumber | 0x80);
            ushort outgoingEndpointID = (ushort)(RequestACK ? EndpointID | 0x8000 : EndpointID);

            // Bytes 0, 1 Sequence number, MSB = 0
            // Bytes 2, 3 Endpoint ID
            // Bytes 4, 5 Expected response size
            // Bytes 6 to N-3 Payload
            // Bytes N-2, N-1 signature, depends on endpoint

            outputBuffer
                .Write(outgoingSequenceNumber)
                .Write(outgoingEndpointID)
                .Write(ExpectedResponseSize)
                .Write(Body.Data)
                .Write(Signature);

            return outputBuffer.Data;
        }
    }
}

namespace ODriveClientLibrary
{
    using System;
    using ODriveClientLibrary.Utilities;

    internal class Response
    {
        public ushort EncodedSequenceNumber { get; private set; }
        public ushort SequenceNumber { get; private set; }
        public int Length { get; private set; }
        public WireBuffer Body { get; private set; } = new WireBuffer(0);

        public static ushort Reverse(ushort input)
        {
            byte[] bytes = BitConverter.GetBytes(input);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public Response(byte[] packetBytes, int packetLength)
        {
            Length = packetLength;

            var responseBuffer = new WireBuffer(packetBytes, Length);

            EncodedSequenceNumber = responseBuffer.Read<ushort>();
            SequenceNumber = (ushort)(EncodedSequenceNumber & 0x7fff);

            Body = new WireBuffer(responseBuffer.ReadArray<byte>((byte)(Length - 2)), Length - 2);
        }
    }
}

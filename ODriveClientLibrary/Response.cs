namespace ODrive
{
    using System;
    using ODrive.Utilities;

    internal class Response
    {
        public ushort SequenceNumber { get; set; }
        public int Length { get; set; }
        public WireBuffer Body { get; set; } = new WireBuffer(0);

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

            // We AND 0x7fff in the Request, server OR's 0x8000 in BidirectionalPacketBasedChannel::process_packet
            var sequenceNumberRaw = responseBuffer.Read<ushort>();
            SequenceNumber = (ushort)((ushort)(sequenceNumberRaw & 0x7fff) | 0x80);
            SequenceNumber = (ushort)(sequenceNumberRaw & 0x7fff);

            Body = new WireBuffer(responseBuffer.ReadArray<byte>((byte)(Length - 2)), Length - 2);
        }
    }
}

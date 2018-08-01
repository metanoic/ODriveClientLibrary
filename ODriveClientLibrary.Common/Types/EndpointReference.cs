namespace ODriveClientLibrary.Common.Types
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct EndpointReference
    {
        [FieldOffset(0)]
        private readonly ushort endpointID;
        public ushort EndpointID => endpointID;

        [FieldOffset(2)]
        private readonly ushort jsonCRC;
        public ushort JsonCRC => jsonCRC;

        public EndpointReference(ushort endpointID, ushort jsonCRC)
        {
            this.endpointID = endpointID;
            this.jsonCRC = jsonCRC;
        }
    }
}

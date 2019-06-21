namespace ODriveClientLibrary.Utilities
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class WireBuffer
    {
        private byte currentIndex = 0;

        public WireBuffer(int size)
        {
            if (size > Config.USB_MAX_PACKET_SIZE)
            {
                throw new NotSupportedException($"Payload cannot be larger than {Config.USB_MAX_PACKET_SIZE}.");
            }

            Data = new byte[size];
        }

        public WireBuffer(byte[] data, int size) : this(size)
        {
            Data = data;
        }

        public byte this[byte index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        public byte[] Data { get; }

        public bool HasData => Data.Length > 0;

        public WireBuffer Write<T>(T value, byte? startIndex = null)
        {
            var dataSize = Marshal.SizeOf<T>();
            var bytes = StructToBytes(value);

            SetIndexIfNeeded(startIndex);

            Buffer.BlockCopy(bytes, 0, Data, currentIndex, bytes.Length);
            currentIndex += (byte)dataSize;

            return this;
        }

        public WireBuffer Write(byte[] value, byte? startIndex = null)
        {
            var dataSize = value.Length;
            var result = new byte[dataSize];

            Buffer.BlockCopy(value, 0, result, 0, dataSize);
            EnsureLittleEndian(typeof(byte), result);

            SetIndexIfNeeded(startIndex);

            Buffer.BlockCopy(result, 0, Data, currentIndex, dataSize);
            currentIndex += (byte)dataSize;

            return this;
        }

        public T Read<T>(byte? startIndex = null)
        {
            var dataSize = Marshal.SizeOf<T>();
            var resultBytes = new byte[dataSize];

            SetIndexIfNeeded(startIndex);

            if (Data.Length == 0)
            {
                return default(T);
            }

            Buffer.BlockCopy(Data, currentIndex, resultBytes, 0, dataSize);
            var result = BytesToStruct<T>(resultBytes);
            currentIndex += (byte)dataSize;

            return result;
        }

        public T[] ReadArray<T>(byte arrLength, byte? startIndex = null)
        {
            var typeSize = Marshal.SizeOf(typeof(T));
            var dataSize = arrLength * typeSize;
            var resultArray = new T[arrLength];

            SetIndexIfNeeded(startIndex);

            for (int i = 0; i < arrLength; i++)
            {
                var rawStructBytes = new byte[typeSize];
                Buffer.BlockCopy(Data, currentIndex, rawStructBytes, 0, typeSize);
                resultArray[i] = BytesToStruct<T>(rawStructBytes);
                currentIndex += (byte)typeSize;
            }

            return resultArray;
        }

        private static void EnsureLittleEndian(Type type, byte[] data, int startOffset = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                // nothing to change
                return;
            }

            foreach (var field in type.GetFields())
            {
                var fieldType = field.FieldType;
                if (field.IsStatic)
                {
                    continue;
                }

                if (fieldType == typeof(string))
                {
                    continue;
                }

                var offset = Marshal.OffsetOf(type, field.Name).ToInt32();

                if (fieldType.IsEnum)
                {
                    fieldType = Enum.GetUnderlyingType(fieldType);
                }

                var subFields = fieldType.GetFields().Where(subField => subField.IsStatic == false).ToArray();

                var effectiveOffset = startOffset + offset;

                if (subFields.Length == 0)
                {
                    Array.Reverse(data, effectiveOffset, Marshal.SizeOf(fieldType));
                }
                else
                {
                    EnsureLittleEndian(fieldType, data, effectiveOffset);
                }
            }
        }

        private static T BytesToStruct<T>(byte[] rawData)
        {
            T result = default(T);

            EnsureLittleEndian(typeof(T), rawData);

            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();
                result = (T)Marshal.PtrToStructure(rawDataPtr, typeof(T));
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        private static byte[] StructToBytes<T>(T data)
        {
            byte[] rawData = new byte[Marshal.SizeOf(data)];
            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();
                Marshal.StructureToPtr(data, rawDataPtr, false);
            }
            finally
            {
                handle.Free();
            }

            EnsureLittleEndian(typeof(T), rawData);

            return rawData;
        }

        private byte SetIndexIfNeeded(byte? newIndex)
        {
            if (newIndex.HasValue)
            {
                currentIndex = newIndex.Value;
            }

            return currentIndex;
        }
    }
}

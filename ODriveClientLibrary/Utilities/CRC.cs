namespace ODriveClientLibrary.Utilities
{
    using System;
    using System.Linq;

    internal class CRC<T> where T : struct, IConvertible
    {
        public T Width { get; private set; }
        private readonly T[] lookupTable;

        public ulong Polynomial { get; private set; }
        public ulong InitialValue { get; private set; }

        // No input or result reflection or final Xor'ing.
        public CRC(T width, ulong polynomial, ulong initialValue)
        {
            Width = width;
            Polynomial = polynomial;
            InitialValue = initialValue;

            lookupTable = CreateLookupTable(Width, Polynomial);
        }

        public T CalculateAsNumeric(byte[] data)
        {
            byte[] checkValue = CalculateCheckValue(data);
            Array.Resize(ref checkValue, 8);
            var longValue = BitConverter.ToUInt64(checkValue, 0);
            return (T)Convert.ChangeType(longValue, typeof(T));
        }

        public byte[] CalculateCheckValue(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("Data cannot be null or empty.");
            }

            ulong crc = InitialValue;
            var widthValue = (ulong)Convert.ChangeType(Width, typeof(ulong));

            foreach (byte b in data)
            {
                var tableValue = lookupTable[((crc >> (int)(widthValue - 8)) ^ b) & 0xFF];
                crc = (ulong)Convert.ChangeType(tableValue, typeof(ulong)) ^ (crc << 8);
                crc &= ulong.MaxValue >> (int)(64 - widthValue);
            }

            return BitConverter.GetBytes(crc).Take((int)(widthValue + 7) / 8).ToArray();
        }

        private static T[] CreateLookupTable(T width, ulong polynomial)
        {
            var result = new T[256];

            var widthValue = (ulong)Convert.ChangeType(width, typeof(ulong));
            ulong topBit = (ulong)1 << (int)(widthValue - 1);

            for (int i = 0; i < result.Length; i++)
            {
                byte input = (byte)i;

                ulong remainder = (ulong)input << (int)(widthValue - 8);

                for (int bitNumber = 0; bitNumber < 8; bitNumber++)
                {
                    if ((remainder & topBit) != 0)
                    {
                        remainder = (remainder << 1) ^ polynomial;
                    }
                    else
                    {
                        remainder = remainder << 1;
                    }
                }

                ulong tableEntry = remainder & (ulong.MaxValue >> (int)(64 - widthValue));

                result[i] = (T)Convert.ChangeType(tableEntry, typeof(T));
            }

            return result;
        }
    }
}

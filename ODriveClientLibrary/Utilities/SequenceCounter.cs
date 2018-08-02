namespace ODriveClientLibrary.Utilities
{
    using System.Threading;

    internal sealed class SequenceCounter
    {
        private int rawValue = ushort.MinValue;
        private ushort currentValue;

        public SequenceCounter(ushort startValue = default)
        {
            currentValue = startValue;
        }

        public ushort NextValue()
        {
            Interlocked.CompareExchange(ref rawValue, ushort.MinValue, ushort.MaxValue);
            return (ushort)(Interlocked.Increment(ref rawValue));
        }
    }
}

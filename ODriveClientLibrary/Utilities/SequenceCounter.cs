namespace ODriveClientLibrary.Utilities
{
    using System.Threading;

    internal sealed class SequenceCounter
    {
        private int currentValue = ushort.MinValue;
        public SequenceCounter(ushort startValue = default)
        {
            currentValue = startValue;
        }

        public ushort NextValue()
        {
            // See https://github.com/metanoic/ODriveClientLibrary/issues/41#issuecomment-409996806 for why we're halving the capacity
            Interlocked.CompareExchange(ref currentValue, ushort.MinValue, short.MaxValue);
            return (ushort)(Interlocked.Increment(ref currentValue));
        }
    }
}

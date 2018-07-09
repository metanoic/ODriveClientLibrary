namespace ODrive.Utilities
{
    using System.Threading;

    internal sealed class ThreadSafeCounter
    {
        private int currentValue = 0;

        public ThreadSafeCounter(int startValue = 0)
        {
            SetValue(startValue);
        }

        public int NextValue()
        {
            Interlocked.Increment(ref currentValue);
            return currentValue;
        }

        public int SetValue(int newValue)
        {
            Interlocked.Exchange(ref currentValue, newValue);
            return currentValue;
        }

        public void Reset()
        {
            currentValue = 0;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace ODrive.Utilities
{
    public static class RetryHelper
    {
        public static async Task<T> ExecuteWithRetry<T>(Func<Task<T>> func, int maxAttempts, TimeSpan? retryDelay = null, Action<int, Exception> onAttemptFailed = null)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            if (maxAttempts < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxAttempts), maxAttempts, "The maximum number of attempts must not be less than 1.");
            }

            var attempt = 0;

            while (true)
            {
                if (attempt > 0 && retryDelay != null)
                {
                    await Task.Delay(retryDelay.Value);
                }

                try
                {
                    //Call the function passed in by the caller. 
                    return await func().ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    attempt++;

                    onAttemptFailed?.Invoke(attempt, exception);

                    if (attempt >= maxAttempts)
                    {
                        throw;
                    }
                }
            }
        }

        public static async Task ExecuteWithRetry(Func<Task> func, int maxAttempts, TimeSpan? retryInterval = null, Action<int, Exception> onAttemptFailed = null)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            async Task<bool> wrapper()
            {
                await func().ConfigureAwait(false);
                return true;
            }

            await ExecuteWithRetry(wrapper, maxAttempts, retryInterval, onAttemptFailed);
        }
    }
}
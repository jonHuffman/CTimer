namespace CTimers
{
    public delegate void TimerCallback();

    public static class TimerCallbackExtensions
    {
        /// <summary>
        /// Automatically performs a null check before invoking the callback
        /// </summary>
        public static void SafeInvoke(this TimerCallback tc)
        {
            if (tc != null)
                tc();
        }
    }
}

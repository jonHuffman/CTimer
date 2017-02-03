namespace CTimers
{
    public static class TimerExtensions
    {
        /// <summary>
        /// Sets the timescale update mode for this timer.
        /// </summary>
        /// <param name="timer">The timer being modified</param>
        /// <param name="updateMode">The update mode you wish to set</param>
        /// <returns>The Timer</returns>
        public static Timer SetTimescale(this Timer timer, UpdateMode updateMode)
        {
            timer.UpdateMode = updateMode;
            return timer;
        }

        public static Timer SetUpdateMode(this Timer timer, UpdateMode updateMode)
        {
            timer.UpdateMode = updateMode;
            return timer;
        }

        public static Timer SetOnComplete(this Timer timer, TimerCallback onComplete)
        {
            timer.OnComplete = onComplete;
            return timer;
        }
    }
}

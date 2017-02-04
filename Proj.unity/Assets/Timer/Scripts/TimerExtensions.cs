namespace CTimers
{
    public static class TimerExtensions
    {
        /// <summary>
        /// Set a callback method to be invoked upon timer completion
        /// </summary>
        /// <param name="timer">The timer being modified</param>
        /// <param name="onComplete">On Complete callback</param>
        /// <returns>The Timer</returns>
        public static Timer SetOnComplete(this Timer timer, TimerCallback onComplete)
        {
            timer.OnComplete = onComplete;
            return timer;
        }

        /// <summary>
        /// Sets the timescale update mode for this timer. See also: <seealso cref="SetUpdateMode(Timer, UpdateMode)"/>
        /// </summary>
        /// <param name="timer">The timer being modified</param>
        /// <param name="updateMode">The update mode you wish to set</param>
        /// <returns>The Timer</returns>
        public static Timer SetTimescale(this Timer timer, UpdateMode updateMode)
        {
            timer.UpdateMode = updateMode;
            return timer;
        }

        /// <summary>
        /// Sets the timescale update mode for this timer. See also: <seealso cref="SetTimescale(Timer, UpdateMode)"/>
        /// </summary>
        /// <param name="timer">The timer being modified</param>
        /// <param name="updateMode">The update mode you wish to set</param>
        /// <returns>The Timer</returns>
        public static Timer SetUpdateMode(this Timer timer, UpdateMode updateMode)
        {
            timer.UpdateMode = updateMode;
            return timer;
        }
    }
}

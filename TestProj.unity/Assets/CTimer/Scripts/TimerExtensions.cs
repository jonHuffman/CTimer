namespace CTimers
{
    public static class TimerExtensions
    {
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

        /// <summary>
        /// Sets the number of times this Timer should loop. OnComplete will be called at the end the timer and all loops. See <see cref="SetOnLoopComplete(Timer, TimerCallback)"/>
        /// <para/>If the set value is less than the Timer's current loop amount the Timer will stop after the current Loop and call onComplete.
        /// <para/>If the set value is greater than the Timer's current loop amount the Timer will continue to loop until it is reached. Provided it has not already completed.
        /// </summary>
        /// <param name="timer">The timer being modified</param>
        /// <param name="loopCount">Number of times to loop the timer. A value of -1 will cause the Timer to loop forever</param>
        /// <returns>The Timer</returns>
        public static Timer SetLoops(this Timer timer, int loopCount)
        {
            timer.TotalLoops = loopCount;
            return timer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="onLoopComplete"></param>
        /// <returns></returns>
        public static Timer SetOnLoopComplete(this Timer timer, TimerCallback onLoopComplete)
        {
            timer.OnLoopComplete = onLoopComplete;
            return timer;
        }

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
    }
}

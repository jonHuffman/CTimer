namespace CTimers
{
    public class Timer
    {
        private float _currentTime;
        private float _endTime;
        private bool _isActive;
        private bool _isComplete;

        private TimerCallback _onLoopComplete;
        private TimerCallback _onComplete;
        private UpdateMode _updateMode;
        private bool _shouldLoop;
        private int _totalLoops;
        private int _loopCount;

        #region Internal

        internal TimerCallback OnLoopComplete
        {
            get { return _onLoopComplete; }
            set { _onLoopComplete = value; }
        }

        internal TimerCallback OnComplete
        {
            get { return _onComplete; }
            set { _onComplete = value; }
        }

        internal UpdateMode UpdateMode
        {
            get { return _updateMode; }
            set { _updateMode = value; }
        }

        internal bool IsLooping
        {
            get { return _totalLoops < 0 || _loopCount < _totalLoops; }
        }

        internal int TotalLoops
        {
            get { return _totalLoops; }
            set { _totalLoops = value; }
        }

        /// <summary>
        /// The amount of time that has passed for the Timer
        /// </summary>
        internal float CurrentTime
        {
            get { return _currentTime; }
        }

        internal Timer()
        {
        }

        internal void Start(float time)
        {
            _endTime = time;
            _currentTime = 0f;
            _loopCount = 0;
            _isComplete = false;
            ActivateTimer();
        }

        /// <summary>
        /// Increments the timer by the amount provided
        /// </summary>
        /// <param name="deltaTime">The delta time of this frame</param>
        internal void Tick(float deltaTime)
        {
            _currentTime += deltaTime;

            if (_isComplete == false && _currentTime >= _endTime)
            {
                Complete();
            }
        }
        #endregion

        /// <summary>
        /// Is this Timer ticking down
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
        }

        /// <summary>
        /// Is the Timer is considered complete once it has finished all loops and any callbacks. 
        /// Completed Timers will no longer update. Should you wish to run it again you must call Restart.
        /// </summary>
        public bool IsComplete
        {
            get { return _isComplete; }
        }

        /// <summary>
        /// Stops the Timer, resetting its progress to 0 and preventing it from updating.
        /// Call Resume to begin countdown once again.
        /// </summary>
        public void Stop()
        {
            _currentTime = 0f;
            _loopCount = 0;
            _isComplete = false;
            DeactivateTimer();
        }

        /// <summary>
        /// Pauses the Time at its current progress point.
        /// Call Resume to begin countdown once again.
        /// </summary>
        public void Pause()
        {
            DeactivateTimer();
        }

        /// <summary>
        /// Resumes the Timer's countdown towards completion.
        /// </summary>
        public void Resume()
        {
            ActivateTimer();
        }

        /// <summary>
        /// Resets a Timer to 0 and begins ticking down immediately.
        /// </summary>
        public void Restart()
        {
            _currentTime = 0f;
            _loopCount = 0;
            _isComplete = false;
            ActivateTimer();
        }

        /// <summary>
        /// Activates the timer. Registers it so that it recieves update ticks
        /// </summary>
        private void ActivateTimer()
        {
            _isActive = true;
            CTimer.RegisterForTicks(this);
        }


        /// <summary>
        /// Deactivates the timer. Unregisters it so that it does not recieve update ticks
        /// </summary>
        private void DeactivateTimer()
        {
            _isActive = false;
            CTimer.UnregisterForTicks(this);
        }

        /// <summary>
        /// Called when a Timer completes.
        /// Restarts the Timer if it is set to loop.
        /// Recycles the Timer if Chronos is set to recycle.
        /// </summary>
        private void Complete()
        {
            // No matter what, we complete at least once, therefore we complete at least one loop
            _loopCount++;
            _onLoopComplete.SafeInvoke();

            if (IsLooping)
            {
                _currentTime = 0f;
                return;
            }

            _onComplete.SafeInvoke();
            DeactivateTimer();
            _isComplete = true;

            if (CTimer.RecycleTimers)
            {
                CTimer.Recycle(this);
            }
        }
    }
}


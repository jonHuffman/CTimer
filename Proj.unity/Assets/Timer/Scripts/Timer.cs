namespace CTimers
{
    public class Timer
	{

		private float _currentTime;
		private float _endTime;

		private bool _shouldLoop;
		private UpdateMode _updateMode;
		private TimerCallback _onComplete;

        internal bool ShouldLoop
        {
            get { return _shouldLoop; }
            set { _shouldLoop = value; }
        }

        internal UpdateMode UpdateMode
        {
            get { return _updateMode; }
            set { _updateMode = value; }
        }

        internal TimerCallback OnComplete
        {
            get { return _onComplete; }
            set { _onComplete = value; }
        }

        internal bool IsAvailable
		{
			get;
			set;
		}

		public Timer()
		{
		}

		public void Start(float time)
		{
			_endTime = time;
		}

        /// <summary>
        /// Increments the timer by the amount provided
        /// </summary>
        /// <param name="deltaTime">The delta time of this frame</param>
		internal void Tick(float deltaTime)
		{
			_currentTime += deltaTime;

			if (_currentTime >= _endTime)
			{
                _onComplete.SafeInvoke();
			}
		}
	}
}


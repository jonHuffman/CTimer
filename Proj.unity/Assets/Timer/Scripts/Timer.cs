namespace CTimers
{
	public class Timer
	{
		private float _currentTime;
		private float _endTime;
		private bool _isActive;
		private bool _isComplete;

		private bool _shouldLoop;
		private UpdateMode _updateMode;
		private TimerCallback _onComplete;

		#region Internal

		internal bool IsAvailable
		{
			get;
			set;
		}

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
			_isActive = true;
			_isComplete = false;
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
				_isComplete = true;
				_onComplete.SafeInvoke();
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
		/// Is the Timer complete and no longer running
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
			_isActive = false;
			_isComplete = false;
		}

		/// <summary>
		/// Pauses the Time at its current progress point.
		/// Call Resume to begin countdown once again.
		/// </summary>
		public void Pause()
		{
			_isActive = false;
		}

		/// <summary>
		/// Resumes the Timer's countdown towards completion.
		/// </summary>
		public void Resume()
		{
			_isActive = true;
		}

		/// <summary>
		/// Resets a Timer to 0 and begins ticking down immediately.
		/// </summary>
		public void Restart()
		{
			_currentTime = 0f;
			_isActive = true;
			_isComplete = false;
		}

		/// <summary>
		/// Called when a Timer completes.
		/// Restarts the Timer if it is set to loop.
		/// Recycles the Timer if Chronos is set to recycle.
		/// </summary>
		private void Complete()
		{
			_isComplete = true;

			if(Chronos.RecycleTimers)
			{
				IsAvailable = true;
			}
		}
	}
}


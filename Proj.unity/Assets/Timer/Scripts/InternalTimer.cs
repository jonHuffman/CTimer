using System;

namespace Chronos
{
	internal class InternalTimer
	{

		private float _currentTime;
		private float _endTime;

		private bool _loop;
		private UpdateMode _updateMode;
		private TimerCallback _onComplete;

		public bool IsAvailable
		{
			get;
			set;
		}

		public InternalTimer()
		{
		}

		public void Start(float time)
		{
			_endTime = _currentTime;
		}

		public void Tick(float time)
		{
			_currentTime += time;

			if (_currentTime >= _endTime)
			{
				if (_onComplete != null)
				{
					_onComplete();
				}
			}
		}

		public void SetUpdateMode(UpdateMode updateMode)
		{
			_updateMode = updateMode;
		}

		public void SetOnComplete(TimerCallback onComplete)
		{
			_onComplete = onComplete;
		}
	}
}


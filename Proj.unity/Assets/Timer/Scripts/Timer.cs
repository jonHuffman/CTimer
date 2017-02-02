using System.Collections.Generic;

namespace Chronos
{
	public delegate void TimerCallback();
	public static class Timer
	{
		private const int DEFAULT_POOL_SIZE = 10;

		private static bool _isInitialized = false;
		private static List<InternalTimer> _timerPool;

		public static InternalTimer Start(float time)
		{
			InternalTimer timer = _timerPool.Find(t => t.IsAvailable);

			if (timer == null)
			{
				timer = new InternalTimer();
				_timerPool.Add(timer);
			}

			timer.Start(time);
			return timer;
		}

		public static InternalTimer SetTimescale(this InternalTimer timer, UpdateMode updateMode)
		{
			timer.SetUpdateMode(updateMode);
		}

		public static InternalTimer OnComplete(this InternalTimer timer, TimerCallback onComplete)
		{
			
		}

		public static void Initialize(int initialPoolSize)
		{
			_isInitialized = true;

			_timerPool = new List<InternalTimer>(initialPoolSize);

			for (int i = 0; i < initialPoolSize; ++i)
			{
				_timerPool.Add(new InternalTimer());
			}
		}

		private static void InitCheck()
		{
			if (_isInitialized)
			{
				return;
			}

			Initialize(DEFAULT_POOL_SIZE);
		}
	}
}
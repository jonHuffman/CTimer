using System.Collections.Generic;

namespace CTimers
{
    public class Chronos
    {
        private const int DEFAULT_POOL_SIZE = 10;

        private static bool _isInitialized = false;
        private static List<Timer> _timerPool;
        private static UnityTicker _ticker;

        /// <summary>
        /// Starts a Timer counting to the set time value.
        /// </summary>
        /// <param name="time">The duration of the Timer</param>
        /// <returns>The Timer</returns>
        public static Timer Start(float time)
        {
            InitCheck();

            Timer timer = _timerPool.Find(t => t.IsAvailable);

            if (timer == null)
            {
                timer = new Timer();
                _timerPool.Add(timer);
            }

            timer.Start(time);
            timer.IsAvailable = false;
            return timer;
        }		

        /// <summary>
        /// Initializes Chronos. Only needs to be called once in the lifecycle of hte application.
        /// </summary>
        /// <param name="initialPoolSize">The initial number of Timers to create in the Pool</param>
        public static void Init(int initialPoolSize)
        {
            _isInitialized = true;

            _timerPool = new List<Timer>(initialPoolSize);

            for (int i = 0; i < initialPoolSize; ++i)
            {
                _timerPool.Add(new Timer());
            }
        }

        /// <summary>
        /// Links the UnityTicker to Chronos so that we can update our Timers
        /// </summary>
        /// <param name="ticker">The ticker to link</param>
        internal static void SetTicker(UnityTicker ticker)
        {
            _ticker = ticker;
            _ticker.SetTickCallback(Tick);
        }

        private static void Tick(float deltaTime, float unscaledDeltaTime)
        {
            Timer t;
            for(int i = 0, count = _timerPool.Count; i < count; ++i)
            {
                t = _timerPool[i];

                if(t.IsAvailable == false)
                {
                    t.Tick(t.UpdateMode == UpdateMode.Normal ? deltaTime : unscaledDeltaTime);
                }
            }
        }

        private static void InitCheck()
        {
            if (_isInitialized)
            {
                return;
            }

            Init(DEFAULT_POOL_SIZE);
        }
    }
}
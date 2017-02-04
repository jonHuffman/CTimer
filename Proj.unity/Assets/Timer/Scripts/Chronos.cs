using System;
using System.Collections.Generic;

namespace CTimers
{
    public class Chronos
    {
        private const int DEFAULT_POOL_SIZE = 10;

        private static bool _isInitialized = false;
        private static UnityTicker _ticker;

        private static bool _recycleTimers;
        private static List<Timer> _timerPool;
        private static List<WeakReference> _unpooledTimers;

        internal static bool RecycleTimers
        {
            get { return _recycleTimers; }
        }

        /// <summary>
        /// Starts a Timer counting to the set time value and returns it for access and modification.
        /// </summary>
        /// <param name="time">The duration of the Timer</param>
        /// <returns>The Timer</returns>
        public static Timer Start(float time)
        {
            InitCheck();

            Timer timer;

            if (_recycleTimers)
            {
                timer = _timerPool.Find(t => t.IsAvailable);

                if (timer == null)
                {
                    timer = new Timer();
                    _timerPool.Add(timer);
                }
            }
            else
            {
                timer = new Timer();
                _unpooledTimers.Add(new WeakReference(timer));
            }

            timer.Start(time);
            timer.IsAvailable = false;
            return timer;
        }

        /// <summary>
        /// Initializes Chronos. Only needs to be called once in the lifecycle of hte application.
        /// </summary>
        /// <param name="initialPoolSize">The initial number of Timers to create in the Pool</param>
        /// <param name="recycleTimers">If true, Timers will automatically return to a bool upon completion. Stopped Timers are not considered completed.</param>
        public static void Init(int initialPoolSize, bool recycleTimers = false)
        {
            _isInitialized = true;
            _recycleTimers = recycleTimers;

            if (_recycleTimers)
            {
                _timerPool = new List<Timer>(initialPoolSize);

                for (int i = 0; i < initialPoolSize; ++i)
                {
                    _timerPool.Add(new Timer());
                }
            }
            else
            {
                _unpooledTimers = new List<WeakReference>();
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

        /// <summary>
        /// Updates all active timers with the provided delat time values
        /// </summary>
        /// <param name="deltaTime">The delta time of the frame</param>
        /// <param name="unscaledDeltaTime">The unscaled delta time of the frame</param>
        private static void Tick(float deltaTime, float unscaledDeltaTime)
        {
            Timer t;

            if (_timerPool != null)
            {
                for (int i = 0, count = _timerPool.Count; i < count; ++i)
                {
                    t = _timerPool[i];

                    if (t.IsActive)
                    {
                        t.Tick(t.UpdateMode == UpdateMode.Normal ? deltaTime : unscaledDeltaTime);
                    }
                }
            }

            if (_unpooledTimers != null)
            {
                for (int i = _unpooledTimers.Count - 1, final = 0; i >= final; i--)
                {
                    if (_unpooledTimers[i].IsAlive == false)
                    {
                        _unpooledTimers.RemoveAt(i);
                        continue;
                    }

                    t = (Timer)_unpooledTimers[i].Target;

                    if (t.IsActive)
                    {
                        t.Tick(t.UpdateMode == UpdateMode.Normal ? deltaTime : unscaledDeltaTime);
                    }
                }
            }
        }

        /// <summary>
        /// Confirms initialization and if uninitialized, performs initialization... initialize the initialization sequence and get this thing initialized!
        /// </summary>
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
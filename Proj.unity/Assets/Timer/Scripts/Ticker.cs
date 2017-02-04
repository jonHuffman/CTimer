using UnityEngine;

namespace CTimers
{
    /// <summary>
    /// Performs the update loop that ticks all timers. Auto-initializes and links to Chronos.
    /// </summary>
    /// <remarks>
    /// Explicitly named different than file in order to prevent Unity from detecting and displaying as a component option within the inspector.
    /// </remarks>
    public class UnityTicker : MonoBehaviour
    {
        /// <summary>
        /// Provides the deltaTime and unscaledDeltaTime for each frame.
        /// </summary>
        public delegate void TickCallback(float deltaTime, float unscaledDeltaTime);

        private TickCallback _tickCallback;

        /// <summary>
        /// Automatically invoked by Unity after Awake.
        /// Automatically links itself to Chronos.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            UnityTicker ticker = new GameObject("UnityTicker").AddComponent<UnityTicker>();
            Chronos.SetTicker(ticker);
        }

        #region Unity

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (_tickCallback != null)
                _tickCallback(Time.deltaTime, Time.unscaledDeltaTime);
        }
        #endregion

        /// <summary>
        /// Sets the TickCallback method to be invoked each update
        /// </summary>
        /// <param name="tickCallback">Provides the deltaTime and unscaledDeltaTime for each frame</param>
        public void SetTickCallback(TickCallback tickCallback)
        {
            _tickCallback = tickCallback;
        }
    }
}

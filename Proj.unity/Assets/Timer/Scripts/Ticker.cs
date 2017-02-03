using UnityEngine;

namespace CTimers
{
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

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (_tickCallback != null)
                _tickCallback(Time.deltaTime, Time.unscaledDeltaTime);
        }

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

using CTimers;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class RecyclingTests : MonoBehaviour
{
	private Timer _timerA;
	private Timer _timerB;

	void Start()
	{
		CTimer.Init(3, true);

		Assert.AreEqual(3, CTimer.AvailableTimers, string.Format("Unexpected number of Available Timers. Expected 3, had {0}", CTimer.AvailableTimers));

		_timerA = CTimer.Start(5f).SetOnComplete(TimerA_Complete);
		_timerB = CTimer.Start(1f).SetOnComplete(TimerB_StepOne);

		Assert.AreEqual(1, CTimer.AvailableTimers, string.Format("Unexpected number of Available Timers. Expected 1, had {0}", CTimer.AvailableTimers));
	}

	private void TimerB_StepOne()
	{
		// Available timers should still be 1, because a Timer does not return to the Pool till after OnComplete
		Assert.AreEqual(1, CTimer.AvailableTimers, string.Format("Unexpected number of Available Timers. Expected 1, had {0}", CTimer.AvailableTimers));

		_timerB = CTimer.Start(1f);

		Assert.AreEqual(0, CTimer.AvailableTimers, "Available Timers should be 0");
		Assert.AreEqual(3, CTimer.ActiveTimers, string.Format("There should be 3 active timers at this point, there are {0}. Timers become complete and inactive after the callback.", CTimer.ActiveTimers));

		// Should create a new Timer in the pool, will verify in TimerA_Complete
		CTimer.Start(2f);
	}

	private void TimerA_Complete()
	{
		// If this is not equal then something went weird in our Timer creation and Pooling
		Assert.AreEqual(3, CTimer.AvailableTimers, string.Format("Unexpected number of Available Timers. Expected 3, had {0}", CTimer.AvailableTimers));

		StartCoroutine(FrameDelay());
	}

	private IEnumerator FrameDelay()
	{
		yield return new WaitForEndOfFrame();

		Assert.AreEqual(4, CTimer.AvailableTimers, "At the end of the test there should be 4 Timers available in the Pool");
		Assert.AreEqual(0, CTimer.ActiveTimers, "At the end of the test there should be 0 Active Timers");

		IntegrationTest.Pass();
	}
}

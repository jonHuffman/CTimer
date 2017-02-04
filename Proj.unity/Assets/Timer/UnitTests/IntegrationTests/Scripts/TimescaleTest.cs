using CTimers;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Tests the UpdateMode modifier to ensure that we can support callbacks in both a Normal and Unscaled deltaTime situation.
/// First we check that unscaled updates work. We then reset timescale and confirm that our timer continues to tick once the time "starts moving" again
/// </summary>
public class TimescaleTest : MonoBehaviour
{
    private bool _normalCalled;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0f;

        _normalCalled = false;

        Chronos.Start(1f).SetOnComplete(NormalOnComplete).SetTimescale(UpdateMode.Normal);
        Chronos.Start(2f).SetOnComplete(UnscaledOnComplete).SetTimescale(UpdateMode.UnscaledTime);
    }

    private void NormalOnComplete()
    {
        _normalCalled = true;

        Assert.AreNotEqual(0, Time.timeScale, "TimeScale should not be 0 at this point.");

        IntegrationTest.Pass();
    }

    private void UnscaledOnComplete()
    {
        Assert.AreEqual(0, Time.timeScale, "TimeScale is not set to 0. Test is invalid");
        Assert.IsFalse(_normalCalled, "The normal TimeScale callback was called despite timescale being set to 0");

        Time.timeScale = 1f;
    }
}

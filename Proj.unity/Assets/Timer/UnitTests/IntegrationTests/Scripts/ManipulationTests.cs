using CTimers;
using UnityEngine;
using UnityEngine.Assertions;

public class ManipulationTests : MonoBehaviour
{
    [SerializeField]
    private bool _recycle = false;

    private Timer _timer;
    
    void Start()
    {
        if(_recycle)
        {
            Chronos.Init(10, true);
        }

        _timer = Chronos.Start(4f).SetOnComplete(MainTimerComplete);
        _timer.Pause();

        Assert.IsFalse(_timer.IsActive, "Timer should not be active");

        _timer.Resume();
        Chronos.Start(1f).SetOnComplete(FirstStep);
    }

    private void FirstStep()
    {
        _timer.Pause();
        
        Assert.IsTrue(_timer.CurrentTime >= 1f, "Step 1: Timer has not progressed as expected.");

        Chronos.Start(1.1f).SetOnComplete(SecondStep);
    }

    private void SecondStep()
    {
        Assert.IsTrue(_timer.CurrentTime >= 1f && _timer.CurrentTime < 2f, "Step 2: Timer has not progressed as expected.");

        _timer.Restart();

        Chronos.Start(2.5f).SetOnComplete(ThirdStep);
    }

    private void ThirdStep()
    {
        Assert.IsTrue(_timer.IsActive, "Step 3: Timer should be active");
        Assert.IsFalse(_timer.IsComplete, "Step 3: Timer should not hace completed yet");

        _timer.Stop();

        Assert.IsFalse(_timer.IsActive, "Step 3: Timer should be inactive");
        Assert.AreEqual(0f, _timer.CurrentTime, "Step 3: Timer is stopped and should not have a current time value");

        _timer.Resume();

        Chronos.Start(2.5f).SetOnComplete(FourthStep);
    }

    private void FourthStep()
    {
        Assert.IsTrue(_timer.IsActive, "Step 4: Timer should be active");
        Assert.IsFalse(_timer.IsComplete, "Step 4: Timer should not hace completed yet");
    }

    private void MainTimerComplete()
    {
        Assert.IsTrue(_timer.IsComplete, "Timer should be complete now");

        IntegrationTest.Pass();
    }
}
using CTimers;
using UnityEngine;
using UnityEngine.Assertions;

public class LoopTests : MonoBehaviour
{
    private const int BASIC_LOOP_TOTAL = 3;
    private const int LOOP_STEP = 4;

    private int _basicLoopCount = 0;
    private int _incLoopCount = 0;
    private int _incLoopsTotal;
    private int _decLoopCount = 0;

    private Timer _increaseLoopsTimer;
    private Timer _decreaseLoopsTimer;

    void Start()
    {
        // Total length: 1f
        _incLoopsTotal = LOOP_STEP;
        _increaseLoopsTimer = CTimer.Start(0.25f).SetLoops(_incLoopsTotal).SetOnLoopComplete(IncLoopComplete).SetOnComplete(OnIncComplete);

        // Total length: 1f
        _decreaseLoopsTimer = CTimer.Start(0.25f).SetLoops(LOOP_STEP).SetOnLoopComplete(DecLoopComplete).SetOnComplete(OnDecComplete);

        // Total length: 0.6f
        CTimer.Start(0.2f).SetLoops(BASIC_LOOP_TOTAL).SetOnLoopComplete(BasicLoopComplete).SetOnComplete(BasicTimerComplete);
        CTimer.Start(0.1f).SetLoops(CTimer.INFINITE_LOOP).SetOnComplete(InfiniteLoopComplete);
    }

    private void BasicLoopComplete()
    {
        _basicLoopCount++;
        Assert.IsTrue(_basicLoopCount <= BASIC_LOOP_TOTAL, "Loop Count should not have exceeded " + BASIC_LOOP_TOTAL);
    }

    private void BasicTimerComplete()
    {
        Assert.AreEqual(BASIC_LOOP_TOTAL, _basicLoopCount, "Loop count mismatch. Initial Timer should be finished looping.");

        // Increase _incLoopsTotal as part of its test
        _incLoopsTotal = LOOP_STEP * 2;
        _increaseLoopsTimer.SetLoops(_incLoopsTotal);

        // Decrease the Loop count for _decreaseLoopsTimer as part of its test
        _decreaseLoopsTimer.SetLoops(LOOP_STEP / 2);
    }

    private void IncLoopComplete()
    {
        _incLoopCount++;
        Assert.IsTrue(_incLoopCount <= _incLoopsTotal, "IncLoops should never exceed its set total");
    }

    private void OnIncComplete()
    {
        Assert.AreEqual(_incLoopCount, _incLoopsTotal, string.Format("Increase Loop count mismatch. Got {0}, expected {1}", _incLoopCount, _incLoopsTotal));

        // This is the longest test, so it will call Success if it passes
        IntegrationTest.Pass();
    }

    private void DecLoopComplete()
    {
        _decLoopCount++;
        Assert.IsTrue(_incLoopCount < LOOP_STEP, "DecLoopComplete: DecLoops should never exceed its set total");
    }

    private void OnDecComplete()
    {
        Assert.IsTrue(_incLoopCount < LOOP_STEP, "OnDecComplete: DecLoops should never exceed its set total");
    }

    private void InfiniteLoopComplete()
    {
        IntegrationTest.Fail("An infinite looping timer should never have OnComplete called");
    }
}

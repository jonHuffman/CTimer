using CTimers;
using UnityEngine;

/// <summary>
/// Tests the OnComplete method of the timer. If OnComplete is invoked the test is passed.
/// </summary>
public class OnCompleteTest : MonoBehaviour
{
    private void Start()
    {
        Chronos.Start(1f).SetOnComplete(TestComplete);
    }

    private void TestComplete()
    {
        IntegrationTest.Pass();
    }
}

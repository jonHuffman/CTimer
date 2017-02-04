using CTimers;
using UnityEngine;

/// <summary>
/// Tests the OnComplete method of the timer. If OnComplete is invoked the test is passed.
/// </summary>
public class OnCompleteTest : MonoBehaviour
{
    [SerializeField]
    private bool _recycle = false;

    private void Start()
    {
        if (_recycle)
        {
            Chronos.Init(10, true);
        }

        Chronos.Start(1f).SetOnComplete(TestComplete);
    }

    private void TestComplete()
    {
        IntegrationTest.Pass();
    }
}

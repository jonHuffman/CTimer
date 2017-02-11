using CTimers;
using UnityEngine;

/// <summary>
/// Initializes CTimer for upcoming tests that require Pooling to be enabled. Testing Sinletons can be challenging.
/// </summary>
public class GeneralRecylingTestIntializer : MonoBehaviour
{
    void Awake()
    {
        CTimer.Init(5, true);
    }
}

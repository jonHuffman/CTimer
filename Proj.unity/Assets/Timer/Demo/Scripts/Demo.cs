using CTimers;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;
        Chronos.Start(2f).SetOnComplete(ChangeColor).SetUpdateMode(UpdateMode.Normal);
    }

    private void ChangeColor()
    {
        _renderer.material.color = Color.red;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowMo = 0.05f;
    public float slowdownLength = 1f;
    private float defaultFixedDelta;

    void Start()
    {
        defaultFixedDelta = Time.fixedDeltaTime;
    }

    void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        Time.fixedDeltaTime += (defaultFixedDelta / slowdownLength) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, defaultFixedDelta);
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowMo;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}

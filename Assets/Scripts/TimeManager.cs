using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimeManager : MonoBehaviour
{
    public float slowMo;

    private float slowdownLength = 1.0f;
    private float defaultFixedDelta;

    void Start()
    {
        defaultFixedDelta = Time.fixedDeltaTime;
    }

    void Update()
    {
        Time.timeScale += (1.0f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);

        Time.fixedDeltaTime += (defaultFixedDelta / slowdownLength) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0.0f, defaultFixedDelta);
    }

    public void DoSlowMotion(float length)
    {
        slowdownLength = length;

        Time.timeScale = slowMo;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}

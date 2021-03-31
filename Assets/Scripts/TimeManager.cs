using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimeManager : MonoBehaviour
{
    public float slowMo;

    private float defaultFixedDelta;

    void Start()
    {
        defaultFixedDelta = Time.fixedDeltaTime;
    }

    void Update()
    {

    }

    public void DoSlowMotion(float length)
    {
        StartCoroutine(StartSlowMotion(length));
    }

    IEnumerator StartSlowMotion(float length)
    {
        float transitionTime = length / 4;

        float elapsedTime = 0;
        while (Time.timeScale > slowMo)
        {
            Time.timeScale -= (1.0f / transitionTime) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);

            Time.fixedDeltaTime -= (defaultFixedDelta / transitionTime) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0.0f, defaultFixedDelta);

            elapsedTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(FullSlowMotion(length));

        yield return null;
    }

    IEnumerator FullSlowMotion(float length)
    {
        float transitionTime = length / 2;

        float elapsedTime = 0;
        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.fixedDeltaTime;

            yield return null;
        }

        StartCoroutine(EndSlowMotion(length));

        yield return null;
    }

    IEnumerator EndSlowMotion(float length)
    {
        float transitionTime = length / 4;

        float elapsedTime = 0;
        while (Time.timeScale < 1.0f)
        {
            Time.timeScale += (1.0f / transitionTime) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);

            Time.fixedDeltaTime += (defaultFixedDelta / transitionTime) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0.0f, defaultFixedDelta);

            elapsedTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}

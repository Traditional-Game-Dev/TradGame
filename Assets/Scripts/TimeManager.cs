using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowMo = 0.25f;
    public float slowdownLength = 1f;

    void FixedUpdate()
    {
        Time.timeScale += (1f / slowdownLength) * Time.deltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowMotion() 
    {
        Time.timeScale = slowMo;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}

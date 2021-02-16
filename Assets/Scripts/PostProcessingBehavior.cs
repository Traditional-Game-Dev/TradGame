using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingBehavior : MonoBehaviour
{
    public PostProcessProfile profile;
    private MotionBlur blur;

    public bool useBlur = false;

    // Start is called before the first frame update
    void Start()
    {
        profile.TryGetSettings<MotionBlur>(out blur);

        if (blur != null) 
        {
            blur.active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useBlur) 
        {
            blur.active = true;
        }
        else 
        {
            blur.active = false;
        }
    }
}

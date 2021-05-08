using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadetoblack : MonoBehaviour
{  
    public bool fadeblack;

    void Awake()
    {
        if(fadeblack)
        {
            //gameObject.GetComponent<Image>().color = Color.clear;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            //gameObject.GetComponent<Image>().color = Color.black;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void Update()
    {
        if(fadeblack)
        {
            // gameObject.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.black, 0.5f);
            gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, 1);
        }
        else
        {
            //gameObject.GetComponent<Image>().color = Color.Lerp(Color.black, Color.clear, 0.5f);
            gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, 1);
        }
    }
}

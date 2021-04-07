using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesSprites : MonoBehaviour
{
    public Image LifeOne;
    public Image LifeTwo;
    public Image LifeThree;

    private Color baseColor;
    private Color usedColor;

    void Start()
    {
        baseColor = LifeOne.color;
        usedColor = Color.clear;
    }

    public void UpdateLivesImage(int num)
    {
        switch (num)
        {
            case 1:
                LifeOne.color = LifeOne.color == baseColor ? usedColor : baseColor;
                break;
            case 2:
                LifeTwo.color = LifeTwo.color == baseColor ? usedColor : baseColor;
                break;
            case 3:
                LifeThree.color = LifeThree.color == baseColor ? usedColor : baseColor;
                break;
            default:
                break;
        }
    }
}

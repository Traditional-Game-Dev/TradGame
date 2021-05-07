using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSprites : MonoBehaviour
{
    public Image HeartOne;
    public Image HeartTwo;
    public Image HeartThree;
    public Image HeartFour;
    public Image HeartFive;

    public Sprite halfHeart;

    private Color baseColor;
    private Color usedColor;

    void Start()
    {
        baseColor = HeartOne.color;
        usedColor = Color.black;
    }

    public void UpdateDashImage(int num)
    {
        switch (num)
        {
            case 1:
                //DashOne.color = DashOne.color == baseColor ? usedColor : baseColor;
                break;
            case 2:
                //DashTwo.color = DashTwo.color == baseColor ? usedColor : baseColor;
                break;
            case 3:
                //DashThree.color = DashThree.color == baseColor ? usedColor : baseColor;
                break;
            default:
                break;
        }
    }
}

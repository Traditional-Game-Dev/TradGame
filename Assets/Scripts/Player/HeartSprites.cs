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

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    void Start()
    {
        
    }

    public void UpdateHeartImage(int num)
    {
        switch (num)
        {
            case 0:
                HeartFive.sprite = emptyHeart;
                break;
            case 1:
                HeartFive.sprite = halfHeart;
                break;
            case 2:
                HeartFive.sprite = fullHeart;
                HeartFour.sprite = emptyHeart;
                break;
            case 3:
                HeartFive.sprite = fullHeart;
                HeartFour.sprite = halfHeart;
                break;
            case 4:
                HeartFour.sprite = fullHeart;
                HeartThree.sprite = emptyHeart;
                break;
            case 5:
                HeartFour.sprite = fullHeart;
                HeartThree.sprite = halfHeart;
                break;
            case 6:
                HeartThree.sprite = fullHeart;
                HeartTwo.sprite = emptyHeart;
                break;
            case 7:
                HeartThree.sprite = fullHeart;
                HeartTwo.sprite = halfHeart;
                break;
            case 8:
                HeartTwo.sprite = fullHeart;
                HeartOne.sprite = emptyHeart;
                break;
            case 9:
                HeartTwo.sprite = fullHeart;
                HeartOne.sprite = halfHeart;
                break;
            case 10:
                HeartOne.sprite = fullHeart;
                break;
            default:
                break;
        }
    }

    public void RefillHearts()
    {
        HeartFive.sprite = fullHeart;
        HeartFour.sprite = fullHeart;
        HeartThree.sprite = fullHeart;
        HeartTwo.sprite = fullHeart;
        HeartOne.sprite = fullHeart;
    }
}

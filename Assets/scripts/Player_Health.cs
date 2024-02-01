using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public Image[] hearts;

    private int heartNumber = 3;

    public Sprite fullHeart;

    public Sprite emptyHeart;

    public void TakeDamage(int damage)
    {
        if (heartNumber - damage <= 0)
        {
            //ded
        }
        else
        {
            
            for (int h = 2; h >= heartNumber - 1; h--)
            {
                hearts[h].sprite = emptyHeart;
            }
            heartNumber -= damage;
        }
    }
    public void FillHearts()
    {
        for(int i = 0; i < 3; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}

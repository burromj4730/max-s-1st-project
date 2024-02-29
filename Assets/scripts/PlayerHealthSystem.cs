using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public int health;

    public int lives;

    public List<Image> hearts = new List<Image>();

    public Sprite fullHealth;

    public Sprite emptyHealth;

    public Text lifeCount;

    public Sprite dameged;

    private bool damagedFace;

    private bool invulnerable;

    public float invulnerablility;

    public float damageforce;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Lives"))
        {
            lives = 3;
            lifeCount.text = "x" + lives;
            return;
        }
        lives = PlayerPrefs.GetInt("Lives");
        lifeCount.text = "x" + lives;
    }

    public void DoDamage(int damage, int direction)
    {
        if (!invulnerable)
        {


            if (health - damage <= 0)
            {
                hearts[0].sprite = emptyHealth;
                hearts[1].sprite = emptyHealth;
                hearts[2].sprite = emptyHealth;
                lives--;
                //cheakpoint spawn
                PlayerPrefs.SetInt("Lives", lives);
            }
            else
            {
                health -= damage;
                switch (health)
                {
                    case 2:
                        hearts[0].sprite = emptyHealth;

                        break;

                    case 1:
                        hearts[0].sprite = emptyHealth;
                        hearts[1].sprite = emptyHealth;

                        break;

                    default:

                        break;
                }
                StartCoroutine("DamagedFace");
                StartCoroutine("Invulnerable");
                GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 1) * damageforce);
            }
        }
    }
    private void LateUpdate()
    {
        if (damagedFace)
        {
            GetComponent<SpriteRenderer>().sprite = dameged;
        }
    }
    IEnumerator DamagedFace()
    {
        damagedFace = true;
        yield return new WaitForSeconds(1f);
        damagedFace = false;
    }
    IEnumerator Invulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerablility);
        invulnerable = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject deathParticals;

    public GameObject camera;

    public float shakeAmount;

    public float shakeDuration;

    bool shakeWhenDed;

    Vector3 originalPosition;

    public Sprite IAMDED;


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

    private void Update()
    {
        if (shakeWhenDed)
        {
            if (shakeDuration > 0)
            {
                camera.transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime;
            }
        }
    }
    public void DoDamage(int damage, int direction)
    {
        if (!invulnerable)
        {


            if (health - damage <= 0)
            {
                health = 0;
                hearts[0].sprite = emptyHealth;
                hearts[1].sprite = emptyHealth;
                hearts[2].sprite = emptyHealth;
                lives--;
                PlayerPrefs.SetInt("Lives", lives);
                PlayerDead();

                if (lives < 0)
                {
                    
                }
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
            }
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 1) * damageforce);
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
    private void PlayerDead()
    {
        GetComponent<playerMovement>().canMove = false;
        GetComponent<Animator>().enabled = false;
        StartCoroutine("ExplodePlayer");
        StartCoroutine("reloadScene");
        GetComponent<SpriteRenderer>().sprite = IAMDED;
        StartCoroutine("SlowMo");
    }
    public IEnumerator ExplodePlayer()
    {
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(fadeToWhite(this.gameObject));
        yield return new WaitForSeconds(0.25f);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Instantiate(deathParticals, transform);
        StartCoroutine(TurnOfSprite(this.gameObject));
        yield return new WaitForSeconds(0.1f);
        originalPosition = camera.transform.localPosition;
        shakeWhenDed = true;
    }
    IEnumerator fadeToWhite(GameObject player)
    {
        Color transp = player.transform.GetChild(2).GetComponent<SpriteRenderer>().color;
        yield return new WaitForSeconds(0.025f);
        transp.a += 0.1f;
        player.transform.GetChild(2).GetComponent<SpriteRenderer>().color = transp;
        if (player.transform.GetChild(2).GetComponent<SpriteRenderer>().color.a != 1)
        {
            StartCoroutine(fadeToWhite(player));
        }
    }
    IEnumerator TurnOfSprite(GameObject player)
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
    }
    public IEnumerator reloadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator SlowMo()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
    }
}
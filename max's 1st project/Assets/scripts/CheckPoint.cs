using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Sprite Spawnpoint;

    public bool playerReached = false;

    public GameObject player;

    public int checkpointNumber;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            playerReached = true;

            PlayerPrefs.SetInt("CheckP1", checkpointNumber);
            GetComponent<SpriteRenderer>().sprite = Spawnpoint;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockeDoor : MonoBehaviour
{
    public int doorNumber;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerKeys>().HasKey(doorNumber))
            {
                Destroy(this.gameObject);
            }

           
        }
    }
}
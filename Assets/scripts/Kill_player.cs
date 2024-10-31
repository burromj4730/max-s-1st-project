using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Kill_player : MonoBehaviour
{
    public bool destroyUponContact = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (destroyUponContact)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (destroyUponContact)
        {
            Destroy(this.gameObject);
        }
    }
}

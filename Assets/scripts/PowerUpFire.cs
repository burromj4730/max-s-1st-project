using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFire : MonoBehaviour

{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<playerMovement>().FirePowerUp();
        Destroy(this.gameObject);
    }
}

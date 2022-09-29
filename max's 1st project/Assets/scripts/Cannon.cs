using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float seePlayerRadius = 10f;

    public float losePlayerRadius = 10f;

    public float fireTimer = 10f;

    public float maxAngle = 10f;

    public float minAngle = 10f;

    public LayerMask mask;

    private Vector3 playerLastPos;

    public GameObject weapon;

    public GameObject bullet;

    public Transform bulletSpawn;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, seePlayerRadius, Vector2.up, 1);

        foreach(RaycastHit2D h in hits)
        {
            if(h.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (playerLastPos == null || playerLastPos != h.collider.gameObject.transform.position)
                {
                    playerLastPos = h.collider.gameObject.transform.position;
                    AimAtPlayer(h.collider.gameObject);

                }
                    if (canShoot)
                {
                    canShoot = false;

                    Instantiate(bullet, bulletSpawn.position, weapon.transform.rotation);

                    StartCoroutine("ShootDelay");
                }
            }
        }

    }

    void AimAtPlayer(GameObject player)
    {
        Vector2 target = new Vector2(player.transform.position.x - weapon.transform.position.x,
            player.transform.position.y - weapon.transform.position.y);

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + -90));
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireTimer);

        canShoot = true;
    }
}

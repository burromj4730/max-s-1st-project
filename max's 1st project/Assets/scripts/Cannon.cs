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

    public Transform pivot;

    private Vector3 pivotPosition;

    private Vector3 playerLastPos;

    // Start is called before the first frame update
    void Start()
    {
        pivotPosition = pivot.position;
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D[] hits = new RaycastHit2D[50];
        //Physics2D.CircleCast(pivotPosition, seePlayerRadius, Vector2.down, mask, hits);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(pivotPosition, seePlayerRadius, Vector2.up, 1);

        foreach(RaycastHit2D h in hits)
        {
            if(h.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (playerLastPos == null || playerLastPos != h.collider.gameObject.transform.position)
                {
                    playerLastPos = h.collider.gameObject.transform.position;
                    AimAtPlayer(h.collider.gameObject);
                }

            }
        }

    }

    void AimAtPlayer(GameObject player)
    {
        float angle = Mathf.Asin((player.transform.position.x - pivotPosition.x) / Vector2.Distance(player.transform.position, pivotPosition));

        transform.RotateAround(pivotPosition, new Vector3(0, 0, 1), transform.rotation.z + (angle * Mathf.Rad2Deg));
    }

}

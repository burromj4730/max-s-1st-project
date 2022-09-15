using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFireBall : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float despawnTimer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * moveSpeed;

        StartCoroutine("DespawnTimer");
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

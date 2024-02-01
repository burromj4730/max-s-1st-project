using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPipeEnableCollidersLate : MonoBehaviour
{
    public List<TransportPipePiece> pipesToDisable = new List<TransportPipePiece>();

    private void Start()
    {
        foreach (TransportPipePiece t in pipesToDisable)
        {
            t.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (TransportPipePiece t in pipesToDisable)
            {
                t.gameObject.GetComponent<Collider2D>().enabled = true;
            }

            StartCoroutine("ReDisableColliders");        
        }
    }

    private IEnumerator ReDisableColliders()
    {
        yield return new WaitForSeconds(30f);
        foreach (TransportPipePiece t in pipesToDisable)
        {
            t.gameObject.GetComponent <Collider2D>().enabled = false;
        }
    }
}

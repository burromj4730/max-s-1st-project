using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z : MonoBehaviour
{
    public float pop;

    public Animator POP;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PopTimer");
    }

    IEnumerator PopTimer()
    {
        yield return new WaitForSeconds(pop);

        StartCoroutine("DestroyTimer");

    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}

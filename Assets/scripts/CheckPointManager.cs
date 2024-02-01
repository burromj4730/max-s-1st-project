using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    CheckPoint[] checkPoints;

    public GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
         
        
            checkPoints = GameObject.FindObjectsOfType<CheckPoint>();
        
        
        if (PlayerPrefs.HasKey("CheckP1"))
        {
            int checkpoint = PlayerPrefs.GetInt("CheckP1");
            foreach (CheckPoint c in checkPoints)
            {
                if (c.checkpointNumber == checkpoint)
                {
                    player.transform.position = c.gameObject.transform.position;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

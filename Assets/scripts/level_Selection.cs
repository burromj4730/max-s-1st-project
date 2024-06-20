using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_Selection : MonoBehaviour
{
    public Material locked;

    public Material unlockedUncomplete;

    public Material completed;

    public enum LevelState
    {
        LOCKED,
        UNLOCKED,
        COMPLETE
    }

    public LevelState MState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

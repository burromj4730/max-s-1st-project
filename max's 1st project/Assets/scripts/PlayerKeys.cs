using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour

{
    private List<bool> obtainedKeys = new List<bool>();

    private void Start()
    {
        Key[] Keys = GameObject.FindObjectsOfType<Key>();
        for (int i = 0; i < 1000; i++)
        {
            obtainedKeys.Add(false);
        }
    }
    public void GotKey(int keyNumber)
    {
        obtainedKeys[keyNumber] = true;
    }

    public bool HasKey(int doorNumber)
    {
        return obtainedKeys[doorNumber];
    }
}

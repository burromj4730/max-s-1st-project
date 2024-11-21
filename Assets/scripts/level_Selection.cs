using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_Selection : MonoBehaviour
{
    public Material locked;

    public Material unlockedUncomplete;

    public Material completed;

    public string levelSaveName;

    public bool unlockedByDefault;

    public int sceneIndex;



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
        if (PlayerPrefs.HasKey(levelSaveName))
        {
            switch (PlayerPrefs.GetInt(levelSaveName))
            {
                case 0:
                    this.GetComponent<MeshRenderer>().material = locked;
                    MState = LevelState.LOCKED;
                    break;
                case 1:
                    this.GetComponent<MeshRenderer>().material = unlockedUncomplete;
                    MState = LevelState.UNLOCKED;
                    break;
                case 2:
                    this.GetComponent<MeshRenderer>().material = completed;
                    MState = LevelState.COMPLETE;
                    break;
                default:
                    break;

            }
        }
        else
        {
            PlayerPrefs.SetInt(levelSaveName, 0);
        }
        if (unlockedByDefault && MState != LevelState.COMPLETE)
        {
            this.GetComponent<MeshRenderer>().material = unlockedUncomplete;
            MState = LevelState.UNLOCKED;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (MState != LevelState.LOCKED)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (MState != LevelState.LOCKED)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
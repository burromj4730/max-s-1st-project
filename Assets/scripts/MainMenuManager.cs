using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void startButton()
    {
        SceneManager.LoadScene(1);
    }

    public void exitButton()
    {
        Application.Quit();
    }
}

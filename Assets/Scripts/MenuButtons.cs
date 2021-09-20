using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has Closed");
    }

    public void StartGame()
    {
        //<-- FIRST SCENE HERE
    }

}

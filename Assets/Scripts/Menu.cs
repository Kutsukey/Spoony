using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayClick()
    {
        SceneManager.LoadScene("Levels");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}

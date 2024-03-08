using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ChangeScenes(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        } else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }
}

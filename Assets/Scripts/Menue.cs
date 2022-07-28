using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menue : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Asteroids");
        Time.timeScale = 1;
        Spawner.asteroidCounter = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

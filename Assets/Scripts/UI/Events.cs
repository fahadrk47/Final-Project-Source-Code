using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        Enemy_manager.Enemies_destroy = 0;
        SceneManager.LoadScene("Level");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void next_level(string level)
    {
        Enemy_manager.Enemies_destroy = 0;
        SceneManager.LoadScene("Level" + level);
    }


}

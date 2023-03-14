using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_system : MonoBehaviour
{
    // Start is called before the first frame update
    public float total_health;
    public float currunt_health;
    public Image health_bar;


    void Start()
    {
        currunt_health = total_health;
    }
    void Update()
    {
        health_bar.fillAmount = currunt_health / total_health;
        if (currunt_health < 1)
        {
            PlayerManager.gameOver = true;
            PlayerPrefs.SetInt("power1", 0);
            PlayerPrefs.SetInt("power2", 0);
            PlayerPrefs.SetInt("power3", 0);
            PlayerPrefs.SetInt("power4", 0);
            PlayerPrefs.SetInt("power5", 0);
        }
    }

    public void take_damage(int amount)
    {

        currunt_health = currunt_health - amount;
    }
}

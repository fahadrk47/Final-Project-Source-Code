using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static bool gameOver;
    public static bool level_clear;
    public GameObject next_level_btn;
    public GameObject sp_button;
    public GameObject gameOverPanel;
    public GameObject power_panel;
    public static bool isGameStarted;
    public bool start_auto = false;
    public GameObject startingText;
    public static int Collected;
    public TextMeshProUGUI GemsText;
    public TextMeshProUGUI Enemies_killed;
    public TextMeshProUGUI Game_status;
    public GameObject[] powers;
    public AudioClip game_over;
    public AudioClip bg_auido;
    public AudioClip boss_audio;
    public AudioClip level_audio;
    public AudioClip power_selection_sound;
    public AudioSource camera_;
    public static bool sp_used = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = start_auto;
        next_level_btn.SetActive(false);
        gameOver = false;
        level_clear = false;
        Time.timeScale = 1;
        Collected = PlayerPrefs.GetInt("TotalScore");
        camera_ = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        camera_.clip = bg_auido;
        camera_.Play();
        sp_button.SetActive(false);

        if(isGameStarted)
        {
            startingText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            if (camera_.clip == bg_auido)
            {
                camera_.clip = level_audio;
                camera_.Play();
                camera_.loop = true;
            }
        }
       
        if (Enemy_manager.add_wave == true)
        {
            if (camera_.clip != boss_audio)
            {
                camera_.clip = boss_audio;
                camera_.Play();
                camera_.loop = true;
            }
            if (!sp_used)
            {
                sp_button.SetActive(true);
            }

        }
        if (sp_used)
        {
            sp_button.SetActive(false);
        }
        if (gameOver || level_clear)
        {
            if (level_clear)
            {

                next_level_btn.SetActive(true);
                Game_status.text = "LEVEL COMPLETED";
                power_panel.SetActive(true);

                for (int j = 0; j < 6; j++)
                {
                    powers[j].SetActive(false);
                }
                powers[Random.Range(0, 2)].SetActive(true);
                powers[Random.Range(2, 4)].SetActive(true);
                powers[Random.Range(4, 6)].SetActive(true);
                if (camera_.clip != power_selection_sound)
                {
                    camera_.clip = power_selection_sound;
                    camera_.Play();
                    camera_.loop = true;
                }

                level_clear = false;
            }
            else
            {
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
                if (camera_.clip != game_over)
                {
                    camera_.clip = game_over;
                    camera_.Play();
                    camera_.loop = false;
                }
            }
            Time.timeScale = 0;


        }


        GemsText.text = "Total Score: " + Collected;

        if (SwipeManager.tap && isGameStarted == false)
        {
            isGameStarted = true;
            startingText.SetActive(false);
            PlayerPrefs.SetInt("TotalScore", 0);
        }
        Enemies_killed.text = Enemy_manager.Enemies_destroy.ToString();
    }
    public void power_manager(int x)
    {
        power_panel.SetActive(false);
        PlayerPrefs.SetInt("power" + x, 1);
    }
    public void level_loader(int level)
    {
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + Collected);
        Enemy_manager.Enemies_destroy = 0;
        SceneManager.LoadScene("Level" + level);


    }
}

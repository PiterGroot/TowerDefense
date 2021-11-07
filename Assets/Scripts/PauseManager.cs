using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private int Pausecount = 0;
    [SerializeField]private KeyCode PauseButton = KeyCode.Escape;
    [SerializeField]private GameObject PauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        Pausecount = 0;
        SlowDownTime(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            Pausecount++;
            SlowDownTime(0f);
        }
        switch (Pausecount)
        {
            case 1:
                PauseScreen.SetActive(true);
                Cursor.visible = true;
                FindObjectOfType<AudioManager>().Stop("Drone");
                break;
            case 2:
                PauseScreen.SetActive(false);
                if (FindObjectOfType<GameManager>().Towermode == false){
                    FindObjectOfType<AudioManager>().Stop("Drone");
                }
                SlowDownTime(1f);
                break;
            case 3:
                Pausecount = 1;
                break;
        }
    }
    public void Resume()
    {
        Pausecount = 2;
        SlowDownTime(1f);
        if (FindObjectOfType<GameManager>().Towermode == false) {
            FindObjectOfType<AudioManager>().Stop("Drone");
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
        Time.timeScale = 1f;
    }
    public void SlowDownTime(float amount)
    {
        Time.timeScale = amount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void PauseController()
    {
        
        if (GameIsPaused)
        {
            Debug.Log("resume");
            Resume();
        }
        else
        {
            Pause();
        }
        //GameIsPaused = !GameIsPaused;
    }
    public void QuitGame()
    {
        Debug.Log("Quited");
        Application.Quit();
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu");
        SceneManager.LoadScene("main_menu");
    }
    private void Update()
    {
            
    }

}

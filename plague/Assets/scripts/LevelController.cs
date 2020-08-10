using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance = null;
    GameObject levelSign;
    int sceneIndex, levelPassed;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //levelSign = GameObject.Find("LevelNumber");
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }
    public void YouWin()
    {
        if (sceneIndex == 3)
            Invoke("loadMainMenu", 1f);
        else
        {
            if (levelPassed < sceneIndex)
                PlayerPrefs.SetInt("LevelPassed", sceneIndex);
            //levelSign.gameObject.SetActive(false);
            Invoke("loadNextLevel", 1f);
        }
    }
    public void youLose()
    {
        //levelSign.gameObject.SetActive(false);
        Invoke("loadMainMenu", 1f);
    }

    void loadNextLevel()
    {
        SceneManager.LoadScene(++sceneIndex);
    }
    void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelMenu : MonoBehaviour
{
    public Button level2Button;
    int levelPassed;

    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        level2Button.interactable = false;

        switch (levelPassed)
        {
            case 1:
                level2Button.interactable = true;
                break;
        }
    }
    public void LevelToLoad(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void resetPlayerPrefs()
    {
        level2Button.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
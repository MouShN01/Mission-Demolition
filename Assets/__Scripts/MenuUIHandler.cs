using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject statsMenu;
    public GameObject volumeSettings;
    public TMP_Text levelOne;
    public TMP_Text levelTwo;
    public TMP_Text levelThree;

    public void NewStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#elif UNITY_WEBGL
    Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
        SaveManager.Instance.SaveData();
    }

    public void ShowStats()
    {
        mainMenu.SetActive(false);
        statsMenu.SetActive(true);
        levelOne.text = "Level 1 thorws - " + SaveManager.Instance.levelsRecs[0];
        levelTwo.text = "Level 2 thorws - " + SaveManager.Instance.levelsRecs[1];
        levelThree.text = "Level 3 thorws - " + SaveManager.Instance.levelsRecs[2];

    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        volumeSettings.SetActive(true);
    }    

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        statsMenu.SetActive(false);
        volumeSettings.SetActive(false);

    }
}

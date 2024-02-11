using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject statsMenu;
    public TMP_Text levelOne;
    public TMP_Text levelTwo;
    public TMP_Text levelThree;

    private void Start()
    {
        SaveManager.Instance.LoadData();
    }
    public void NewStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ShowStats()
    {
        mainMenu.SetActive(false);
        statsMenu.SetActive(true);
        levelOne.text = "Level 1 thorws - " + SaveManager.Instance.levelsRecs[0];
        levelTwo.text = "Level 2 thorws - " + SaveManager.Instance.levelsRecs[1];
        levelThree.text = "Level 3 thorws - " + SaveManager.Instance.levelsRecs[2];

    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        statsMenu.SetActive(false);
    }
}

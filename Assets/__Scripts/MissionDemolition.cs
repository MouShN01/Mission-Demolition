using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}
public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Set in Inspector")]
    public TMP_Text uitLevel;
    public TMP_Text uitShots;
    public TMP_Text uitButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    private void Start()
    {
        S = this;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if(castle != null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Disabled");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        GameObject[] bgos = GameObject.FindGameObjectsWithTag("Broken");
        foreach (GameObject go in bgos)
        {
            Destroy(go);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }
    private void Update()
    {
        UpdateGUI();

        if((mode == GameMode.playing) && Goal.goalMet)
        {
            SaveManager.Instance.levelsRecs[level] = shotsTaken;
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 5f);
        }
    }

    void NextLevel()
    {
        level++;
        if(level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if(eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch(showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }
    public static void ShotFired()
    {
        S.shotsTaken++;
    }

    public void BackToMainMenu()
    {
        SaveManager.Instance.SaveData();
        SceneManager.LoadScene(0);
    }
}

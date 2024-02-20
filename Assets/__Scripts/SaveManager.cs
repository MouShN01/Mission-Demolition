using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public List<int> levelsRecs = new List<int>(3);
    public static SaveManager Instance { get; private set; }

    public float generalVolume;
    public float musicVolume;
    public float effectsVolume;

    private void Awake()
    {
        if(Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    class Data
    {
        public List<int> levels;
        public float generalV;
        public float effectsV;
        public float musicV;
    }

    public void SaveData()
    {
        Data data = new Data();
        data.levels = levelsRecs;
        data.generalV = generalVolume;
        data.musicV = musicVolume;
        data.effectsV = effectsVolume;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData() 
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            Data data = JsonUtility.FromJson<Data>(json);

            levelsRecs = data.levels;
            generalVolume = data.generalV;
            musicVolume = data.musicV;
            effectsVolume = data.effectsV;
        }
    }
}

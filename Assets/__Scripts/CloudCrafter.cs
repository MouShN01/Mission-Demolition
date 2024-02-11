using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMult = 0.5f;

    private GameObject[] cloudInstances;

    private void Awake()
    {
        //Масив для хранения облаков
        cloudInstances = new GameObject[numClouds];
        //Поиск CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");
        //Создание облаков в цикле
        GameObject cloud;
        for(int i = 0; i < numClouds; i++)
        {
            //Создание екземпляра облака
            cloud = Instantiate<GameObject>(cloudPrefab);
            //Выбор местоположения для облака
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //Масштабирование облака
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //Чем меньше облако, тем ниже оно должно быть
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //и дальше
            cPos.z = 100 - 90 * scaleU;
            //Применение полученых значений и коордмнат к облаку
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //Сделать облако дочерним по отношению к anchor
            cloud.transform.SetParent(anchor.transform);

            //Добавление облака в массив
            cloudInstances[i] = cloud;
        }
    }

    private void Update()
    {
        //Обход в цикле все создвнные облака
        foreach (GameObject cloud in cloudInstances)
        {
            //Получение масштаба и координат облака
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //Увеличение скорости для ближних облаков
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            //В случае смещения облака слишком влево
            if(cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }

            cloud.transform.position = cPos;

        }
    }
}

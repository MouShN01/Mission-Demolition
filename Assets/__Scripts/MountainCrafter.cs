using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MountainCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numMountains = 40;
    public GameObject mountPrefab;
    public Vector3 mountPosMin = new Vector3(-50, -5, -10);
    public Vector3 mountPosMax = new Vector3(150, -5, -10);
    public int mountMinScale = 1;
    public int mountMaxScale = 3;

    private GameObject[] mountInstances;

    private void Awake()
    {
        mountInstances = new GameObject[numMountains];

        GameObject anchor = GameObject.Find("MountainAnchor");

        GameObject mountain;
        for(int i = 0; i < numMountains; i++)
        {
            mountain = Instantiate<GameObject>(mountPrefab);
            Vector3 mPos = Vector3.zero;
            mPos.x = Random.Range(mountPosMin.x, mountPosMax.x);

            float scaleM = Random.value;
            float scaleVal = Mathf.Lerp(mountMinScale, mountMaxScale, scaleM);

            mPos.y = -3 + scaleVal - mountMinScale;
            mPos.z = 100 * scaleM + 10;

            mountain.transform.localPosition = mPos;
            mountain.transform.localScale = Vector3.one * scaleVal;

            mountain.transform.SetParent(anchor.transform);

            mountInstances[i] = mountain;
        }
    }

    private void Update()
    {
        foreach(GameObject mountain in mountInstances)
        {
            float scaleVal = mountain.transform.localScale.x;
            Vector3 mPos = mountain.transform.position;
            mountain.transform.position = mPos;
        }
    }
}

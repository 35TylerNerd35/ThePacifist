using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]int saveIndex;
    [SerializeField]SavePointScriptable[] savePoints;

    void Awake()
    {
        //Grab save index
        saveIndex = PlayerPrefs.GetInt("SaveIndex", 0);

        //Grab saved inventory stats
        Object[] allSavedVars = Resources.LoadAll("SaveData");
        savePoints = new SavePointScriptable[allSavedVars.Length];
        allSavedVars.CopyTo(savePoints, 0);

        //Assign values
        Vector3 playerSpawnPos = savePoints[saveIndex].playerPos;
        Quaternion playerRot = savePoints[saveIndex].playerRot;

        //Set player to pos
        transform.GetChild(0).position = playerSpawnPos;
        transform.GetChild(0).rotation = playerRot;
        transform.GetChild(0).gameObject.SetActive(true);

        MonoBehaviour[] scripts = transform.GetChild(0).GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }
}

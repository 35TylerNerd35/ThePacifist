using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]int saveIndex;
    [SerializeField]SavePointScriptable[] savePoints;

    void OnEnable()
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
        transform.position = playerSpawnPos;
        transform.rotation = playerRot;

        //Destroy self
        Destroy(this);
    }
}

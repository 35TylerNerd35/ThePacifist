using UnityEngine;

[CreateAssetMenu(fileName = "SavePoint", menuName = "Data/SavePointData", order = 1)]
public class SavePointScriptable : ScriptableObject
{
    //Default Management
    public int savePointIndex;
    public Vector3 playerPos;
    public Quaternion playerRot;

    //Inventory management
    public bool isBatonUnlocked;
    public bool isTaserUnlocked;
}
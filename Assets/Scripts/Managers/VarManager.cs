using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructName{
    public string structString;
    public int structInt;
    public float structFloat;
    public bool structBool;
    public Vector3 structVector3;
    public GameObject structGameObject;
    public Transform structTransform;

    public int[] structIntArray;
}

public class VarManager : MonoBehaviour
{
    //Player stats
    public static float playerHealth, playerStamina;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera playerCam;

    void Awake()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(playerCam.transform);
    }
}

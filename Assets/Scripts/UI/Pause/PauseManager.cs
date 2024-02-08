using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject hud;
    Transform mainCam;

    void Awake()
    {
        mainCam = Camera.main.transform;
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        if(hud == null)
            return;
        
        hud.SetActive(false);
        mainCam.parent.GetComponent<CameraController>().enabled = false;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        if(hud == null)
            return;

        hud.SetActive(true);
        mainCam.parent.GetComponent<CameraController>().enabled = true;
    }
}

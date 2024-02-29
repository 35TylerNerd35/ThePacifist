using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        //Disable player input
        Object[] inputs = FindObjectsOfType(typeof(PlayerInput));
        foreach(PlayerInput input in inputs)
            input.enabled = false;

        //Ensure pause still works
        transform.parent.GetComponent<PlayerInput>().enabled = true;

        if(hud == null)
            return;
        
        hud.SetActive(false);
        // mainCam.parent.GetComponent<CameraController>().enabled = false;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        //Re-enable player input
        Object[] inputs = FindObjectsOfType(typeof(PlayerInput));
        foreach(PlayerInput input in inputs)
            input.enabled = true;

        if(hud == null)
            return;

        hud.SetActive(true);
        // mainCam.parent.GetComponent<CameraController>().enabled = true;
    }
}

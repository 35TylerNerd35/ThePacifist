using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject hud;
    [SerializeField] Transform player;
    Quaternion playerRot;
    Quaternion camRot;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        hud.SetActive(false);
        playerRot = player.rotation;
        camRot = player.GetChild(0).rotation;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        hud.SetActive(true);

        player.rotation = playerRot;
        player.GetChild(0).rotation = camRot;
    }
}

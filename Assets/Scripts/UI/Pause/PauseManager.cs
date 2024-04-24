using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject hud;

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
    }

    public void QuitMain()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseActivator : MonoBehaviour
{
    public BaseInputController inputController;
    InputAction escape;

    bool isPaused;

    void Awake()
    {
        //Init new input system instance
        inputController = new BaseInputController();
    }

    void OnEnable()
    {
        //Enable controller
        inputController.Enable();

        //Enable input actions
        escape = inputController.MenuInputs.Menu;
        escape.Enable();

        escape.started += context => { SetPaused(!isPaused); };
    }

    void OnDisable()
    {
        //Disable inputs
        escape.Disable();
        inputController.Disable();
    }

    public void SetPaused(bool pausedState)
    {
        isPaused = pausedState;
        transform.GetChild(0).gameObject.SetActive(isPaused);
    }
}

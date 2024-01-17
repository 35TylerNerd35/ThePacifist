using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindScript : MonoBehaviour
{
    public BaseInputController inputController;
    InputAction temp;

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
        temp = inputController.PlayerInputs.Interact;
        temp.Enable();
    }

    public void RemapButtonClicked(InputActionReference actionToRebind)
    {
        Debug.Log("REBIND START");

        temp.Disable();

        var rebindOperation = temp.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .WithCancelingThrough("<keyboard>/escape")
                    .OnMatchWaitForAnother(0.4f)
                    .Start();

        actionToRebind.action.ChangeBindingWithPath(temp.ToString());

        Debug.Log("REBIND");

        temp.Enable();
    }

    void OnDisable()
    {
        //Disable the entire controller
        inputController.Disable();
        temp.Disable();
    }

    void Update()
    {
        if(temp.ReadValue<float>() != 0)
        {
            Debug.Log("KEY");
        }
    }
}

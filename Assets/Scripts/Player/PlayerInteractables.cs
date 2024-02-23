using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractables : MonoBehaviour
{
    public static bool isNotInteractableGlobal;

    public BaseInputController inputController;
    InputAction interact;

    [SerializeField] float range = 2f;
    [SerializeField] LayerMask interactionMask;
    [SerializeField] bool isActive;

    GameObject lastPrompt;

    void Awake()
    {
        inputController = new BaseInputController();
    }

    void OnEnable()
    {
        //Enable controller
        inputController.Enable();

        //Enable input actions
        interact = inputController.PlayerInputs.Interact;
        interact.Enable();
    }

    void OnDisable()
    {
        //Disable inputs
        interact.Disable();

        //Disable the entire controller
        inputController.Disable();
    }

    void Update()
    {
        //Grab interact input
        float interactVal = interact.ReadValue<float>();

        //Check if player is close
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, .5f, transform.forward, out hit, range, interactionMask))
        {
            IInteract[] interactObj = hit.transform.gameObject.GetComponents<IInteract>();
            //Check if object can be interacted with
            if(interactObj != null)
            {
                //Enable prompt
                if(lastPrompt == null)
                {
                    lastPrompt = hit.transform.GetChild(0).gameObject;
                    lastPrompt.SetActive(true);
                }
                
                if(interactVal != 0)
                {
                    //Interact with all interfaces
                    for(int i = 0; i < interactObj.Length; i++)
                    {
                        interactObj[i].Interaction();
                    }
                    
                }
            }
        }
        else if(lastPrompt != null)
        {
            lastPrompt.SetActive(false);
            lastPrompt = null;
        }
    }
}

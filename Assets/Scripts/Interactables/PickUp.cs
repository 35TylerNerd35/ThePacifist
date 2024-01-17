using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour, IInteract
{
    public static bool isHolding;
    bool isHeldObject;

    public BaseInputController inputController;
    InputAction throwHeld;

    Transform playerCam;
    GameObject player;

    [SerializeField] float speed = 5f;
    [SerializeField] Transform aimPoint;
    [SerializeField] float throwForce = 500f;

    void Awake()
    {
        inputController = new BaseInputController();
    }

    void OnEnable()
    {
        //Enable controller
        inputController.Enable();

        //Enable input actions
        throwHeld = inputController.PlayerInputs.Attack;
        throwHeld.Enable();
    }

    void OnDisable()
    {
        //Disable inputs
        throwHeld.Disable();

        //Disable the entire controller
        inputController.Disable();
    }

    public void Interaction()
    {
        if(isHeldObject)
        {
            //Activate gravity
            GetComponent<Rigidbody>().useGravity = true;

            //Revert variables
            isHolding = false;
            isHeldObject = false;

            //Re-enable player collision
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
        else
        {
            if(!isHolding)
            {
                //Grab player cam
                playerCam = Camera.main.transform;

                //Grab aim pos
                aimPoint = playerCam.GetChild(0);

                //Setup variables
                isHolding = true;
                isHeldObject = true;

                //Deactivate gravity
                GetComponent<Rigidbody>().useGravity = false;

                //Prevent collisions with player
                player = GameObject.FindGameObjectWithTag("Player");     
                Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }

    void Update()
    {
        if(isHeldObject)
        {
            var step =  speed * Time.deltaTime;

            //Move to hold point
            transform.position = Vector3.MoveTowards(aimPoint.position, playerCam.position, step);

            if(throwHeld.ReadValue<float>() > 0)
            {
                //Activate gravity
                GetComponent<Rigidbody>().useGravity = true;

                //Revert variables
                isHolding = false;
                isHeldObject = false;
                
                //Add forwards force
                GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * throwForce);

                //Re-enable player collision
                Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }
        }
    }
}

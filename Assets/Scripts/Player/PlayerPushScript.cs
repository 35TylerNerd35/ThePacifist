using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPushScript : MonoBehaviour
{
    public BaseInputController inputController;
    InputAction push;

    [SerializeField] Transform hand;
    [SerializeField] Transform handStartPos;
    [SerializeField] Transform handPushPos;
    [SerializeField] Transform handShovePos;
    [Space]
    [SerializeField] float pushForce;
    [SerializeField] float shoveForce;

    float pushHoldTime;
    float positionCheckMargin = .1f;
    bool isMouseButtonDown, isMovingHand, isShoving, hasReleasedPush;

    void Awake()
    {
        inputController = new BaseInputController();
    }

    void OnEnable()
    {
        //Enable controller
        inputController.Enable();

        //Enable input actions
        push = inputController.PlayerInputs.Attack;
        push.Enable();

        hasReleasedPush = true;
    }

    void OnDisable()
    {
        //Disable inputs
        push.Disable();

        //Disable the entire controller
        inputController.Disable();
    }

    void Update()
    {
        CheckPushInput();
        CheckHandPosition();

        if(isMovingHand)
        {
            if(isShoving)
            {
                Shove();
            }
            else
            {
                Push();
            }
        }
    }

    void CheckPushInput()
    {
        if(push.ReadValue<float>() == 0)
        {
            isMouseButtonDown = false;

            if(!hasReleasedPush)
            {
                //Check if shoving
                if(pushHoldTime > .5f)
                {
                    isShoving = false;
                    isMovingHand = false;

                    //Reset hand
                    hand.gameObject.SetActive(false);
                    hand.position = handStartPos.position;
                }
                else
                {
                    isShoving = true;
                }

                //Reset timer
                pushHoldTime = 0;

                hasReleasedPush = true;
            }
        }
        else
        {
            //First instance of input
            if(!isMouseButtonDown)
            {
                hasReleasedPush = false;
                isMouseButtonDown = true;
                isMovingHand = true;

                //Enable hand
                hand.gameObject.SetActive(true);
            }

            //Time how long button held down
            pushHoldTime += Time.deltaTime;
        }
    }

    void Push()
    {
        // Calculate the new position
        Vector3 targetDir = handPushPos.position - hand.position;
        Vector3 newPosition = hand.position + targetDir * pushForce * Time.deltaTime;

        // Use MovePosition to set the new position
        hand.GetComponent<Rigidbody>().MovePosition(newPosition);
    }

    void Shove()
    {
        // Calculate the new position
        Vector3 targetDir = handShovePos.position - hand.position;
        Vector3 newPosition = hand.position + targetDir * shoveForce * Time.deltaTime;

        // Use MovePosition to set the new position
        hand.GetComponent<Rigidbody>().MovePosition(newPosition);
    }

    void CheckHandPosition()
    {
        if(isShoving)
        {
            //Reset hand if hand reaches final shove pos
            if(Vector3.Distance(hand.position, handShovePos.position) < positionCheckMargin)
            {
                isShoving = false;
                isMovingHand = false;

                //Reset hand
                hand.gameObject.SetActive(false);
                hand.position = handStartPos.position;
            }
        }
        else
        {
            if(Vector3.Distance(hand.position, handPushPos.position) < positionCheckMargin)
            {
                isMovingHand = false;
            }
        }
    }
}

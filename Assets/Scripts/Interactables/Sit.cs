using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sit : MonoBehaviour, IInteract
{
    Transform player;
    Transform playerCam;

    bool isSitting;
    public bool isClosing;

    [Header("UI Anim Vals")]
    [SerializeField] float animDelaySpeed = .4f;
    [SerializeField] float zoomSpeed = 7f;
    [SerializeField] float zoomTime = 1.2f;

    [Header("References")]
    [SerializeField] Transform targetPos;
    [SerializeField] Transform sitPos;
    [Space]
    [SerializeField] GameObject UIObj;

    Vector3 previousPlayerPos;
    Vector3 previousPlayerCamPos;
    Vector3 previousPlayerCamRot;

    bool isZooming;

    void Awake()
    {
        isSitting = false;

        playerCam = Camera.main.transform;
        player = GameObject.FindWithTag("Player").transform;
    }

    void OnEnable()
    {
        UIObj.SetActive(false);
    }

    public void Interaction()
    {
        //Disable player input
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<CameraController>().enabled = false;

        player.GetChild(1).gameObject.SetActive(false);

        //Disable camera children
        foreach(Transform child in playerCam)
        {
            child.gameObject.SetActive(false);
        }

        //Enable UI
        UIObj.SetActive(true);

        //Unlock cursor
        Cursor.lockState = CursorLockMode.None;

        //Set previous pos
        previousPlayerPos = player.position;

        //Set previous position & rotation of camera to return to
        previousPlayerCamPos = playerCam.localPosition;
        previousPlayerCamRot = playerCam.eulerAngles;

        //Set new pos
        player.position = sitPos.position;
        isSitting = true;

        StartCoroutine(ZoomToScreen());
    }

    void CloseConsole()
    {
        player.GetChild(1).gameObject.SetActive(true);

        //Zoom out of screen
        playerCam.localPosition =  previousPlayerCamPos;
        playerCam.eulerAngles = previousPlayerCamRot;

        //Set new pos
        player.position = previousPlayerPos;

        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Enable player input
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<CameraController>().enabled = true;

        //Enable camera children
        foreach(Transform child in playerCam)
        {
            child.gameObject.SetActive(true);
        }


        UIObj.SetActive(false);

        isSitting = false;
    }

    void Update()
    {
        if(isZooming)
        {
            float moveStep = zoomSpeed * Time.deltaTime;

            if(isSitting)
            {
                //Zoom in to screen
                Vector3 targetCamPos = new Vector3(targetPos.position.x, targetPos.position.y, targetPos.position.z);
                playerCam.position = Vector3.MoveTowards(playerCam.position, targetCamPos, moveStep);
                playerCam.eulerAngles = targetPos.eulerAngles;
            }
        }
        else if(isClosing)
        {
            isClosing = false;
            isSitting = true;
            CloseConsole();
        }
    }

    IEnumerator ZoomToScreen()
    {
        yield return new WaitForSeconds(animDelaySpeed);
        isZooming  = true;
        yield return new WaitForSeconds(zoomTime);
        //Return to perspective
        isZooming = false;
    }

    IEnumerator PauseMenuDelay()
    {
        yield return new WaitForSeconds(2);
    }
}

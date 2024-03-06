using System.Collections;
using UnityEngine;
using TyeUtils;
using UnityEngine.InputSystem;

public class NewSit : MonoBehaviour, IInteract
{
    Transform player;
    Transform playerCam;

    [SerializeField] float zoomTime = 1.2f;
    [Header("References")]
    [SerializeField] Transform targetPos;
    [SerializeField] Transform sitPos;
    [SerializeField] GameObject UIObj;

    Vector3 previousPlayerPos;
    Vector3 previousPlayerRot;
    Vector3 previousPlayerCamPos;
    Vector3 previousPlayerCamRot;

    GameObject pauseMenu;

    void Awake()
    {
        player = GameObject.FindWithTag("PlayerParent").transform.GetChild(0);
        pauseMenu = GameObject.FindWithTag("PauseParent");
    }   

    void OnEnable() => UIObj.SetActive(false);

    public void Interaction()
    {


        EnableInput(false);

        pauseMenu.SetActive(false);
        player.GetChild(1).gameObject.SetActive(false);


        playerCam = Camera.main.transform;
        foreach(Transform child in playerCam)
            child.gameObject.SetActive(false);

        //Set previous Vectors
        previousPlayerPos = player.position;
        previousPlayerRot = player.eulerAngles;
        previousPlayerCamPos = playerCam.position;
        previousPlayerCamRot = playerCam.eulerAngles;

        //Tween values
        TweenUtils tweenUtils = new();
        tweenUtils.StartVector3Tween(this, playerCam.position, targetPos.position, val => {playerCam.position = val;}, zoomTime);
        TweenUtils tweenUtils2 = new();
        tweenUtils2.StartVector3Tween(this, playerCam.eulerAngles, targetPos.eulerAngles, val => {playerCam.eulerAngles = val;}, zoomTime);
        TweenUtils tweenUtils3 = new();
        tweenUtils3.StartVector3Tween(this, player.position, sitPos.position, val => {player.position = val;}, zoomTime);
        TweenUtils tweenUtils4 = new();
        tweenUtils4.StartVector3Tween(this, player.eulerAngles, sitPos.eulerAngles, val => {player.eulerAngles = val;}, zoomTime);
    }

    public void CloseConsole()
    {
        player.GetChild(1).gameObject.SetActive(true);

        //Tween values
        TweenUtils tweenUtils = new();
        tweenUtils.StartVector3Tween(this, playerCam.position, previousPlayerCamPos, val => {playerCam.position = val;}, zoomTime);
        TweenUtils tweenUtils2 = new();
        tweenUtils2.StartVector3Tween(this, playerCam.eulerAngles, previousPlayerCamRot, val => {playerCam.eulerAngles = val;}, zoomTime);
        TweenUtils tweenUtils3 = new();
        tweenUtils3.StartVector3Tween(this, player.position, previousPlayerPos, val => {player.position = val;}, zoomTime);
        TweenUtils tweenUtils4 = new();
        tweenUtils4.StartVector3Tween(this, player.eulerAngles, previousPlayerRot, val => {player.eulerAngles = val;}, zoomTime);

        tweenUtils4.TweenFinished += FinishUnzoom;
    }

    void FinishUnzoom()
    {
        EnableInput(true);

        player.GetChild(1).gameObject.SetActive(true);
        pauseMenu.SetActive(true);

        //Enable camera children
        foreach(Transform child in playerCam)
        {
            child.gameObject.SetActive(true);
        }
    }

    void EnableInput(bool isEnabled)
    {
        // player = playerParent.GetChild(0);

        UIObj.SetActive(!isEnabled);

        float gravity = PlayerController.gravity;

        if(gravity == -9.8f)
            Camera.main.transform.GetComponent<Animator>().enabled = isEnabled;
        else
            Camera.main.transform.GetComponent<Animator>().enabled = false;

        player.GetComponent<CharacterController>().enabled = isEnabled;
        player.GetComponent<PlayerController>().enabled = isEnabled;
        player.GetComponent<CameraController>().enabled = isEnabled;

        player.GetComponent<PlayerInput>().enabled = isEnabled;
    }
}

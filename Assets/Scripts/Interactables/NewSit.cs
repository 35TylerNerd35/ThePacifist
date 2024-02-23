using System.Collections;
using UnityEngine;
using TyeUtils;

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
        playerCam = Camera.main.transform;
        player = GameObject.FindWithTag("Player").transform;

        pauseMenu = GameObject.FindWithTag("PauseParent");
    }   

    void OnEnable() => UIObj.SetActive(false);

    public void Interaction()
    {
        //Disable player input
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<CameraController>().enabled = false;

        pauseMenu.SetActive(false);

        player.GetChild(1).gameObject.SetActive(false);

        //Disable camera children
        foreach(Transform child in playerCam)
        {
            child.gameObject.SetActive(false);
        }

        //Enable UI
        UIObj.SetActive(true);

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
        UIObj.SetActive(false);

        //Enable player input
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<CameraController>().enabled = true;

        player.GetChild(1).gameObject.SetActive(true);
        pauseMenu.SetActive(true);

        //Enable camera children
        foreach(Transform child in playerCam)
        {
            child.gameObject.SetActive(true);
        }
    }
}

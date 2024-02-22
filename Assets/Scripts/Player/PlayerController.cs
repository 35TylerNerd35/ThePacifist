using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TyeUtils;

public enum TweenVars{
    Walking,
    Run,
    Crouch,
    CrouchRun,
    Dash
}

[System.Serializable]
public struct speedStates{
    public string stateName;
    public float speed;
    public float fovVal;
    public float speedLineAlpha;
    public Vector3 speedLineScale;
    public AnimationClip headBob;
}

public class PlayerController : MonoBehaviour
{
    public CharacterController charC;
    public BaseInputController inputController;
    InputAction move;
    InputAction jump;
    InputAction run;
    InputAction crouch;
    InputAction interact;
    InputAction dash;

    [Header("Movement Stats")]
    [SerializeField] public static float gravity = -9.8f;
    [SerializeField] public static float jumpHeight = 2f;
    [Space]
    [SerializeField] float headBob;

    [Header("Tween Stats")]
    [SerializeField] Image speedLines;
    [SerializeField] float tweenTime;
    [Tooltip("default, run, crouch, crouchRun, dash")]
    [SerializeField] speedStates[] myStats;

    TweenUtils fovTweener = new();
    TweenUtils alphaTweener = new();
    TweenUtils scaleTweener = new();
    
    float currentFOV;

    [Header("Booster Boots")]
    [SerializeField] float dashTweenTime;
    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    [SerializeField] float cooldownTime;
    [SerializeField] Color staminaDefaultColor;
    [SerializeField] Color staminaUsingColor;
    [SerializeField] Image staminaBar;

    bool canDash;
    bool isDashing;

    float speed;
    Vector3 velocity;
    bool isPlayerFloating;

    [Header("Jump")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance  = .4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    void Awake()
    {
        //Init new input controller instance
        inputController = new BaseInputController();

        //Ensure normal gravity on game start
        gravity = -9.8f;

        canDash = true;
    }

    void OnEnable()
    {
        //Enable controller
        inputController.Enable();

        //Enable input actions
        move = inputController.PlayerInputs.Move;
        move.Enable();

        jump = inputController.PlayerInputs.Jump;
        jump.Enable();

        run = inputController.PlayerInputs.Run;
        run.Enable();

        crouch = inputController.PlayerInputs.Crouch;
        crouch.Enable();

        interact = inputController.PlayerInputs.Interact;
        interact.Enable();
        
        dash = inputController.PlayerInputs.Dash;
        dash.Enable();
    }

    void OnDisable()
    {
        //Disable inputs
        move.Disable();
        jump.Disable();
        run.Disable();
        crouch.Disable();
        dash.Disable();

        //Disable the entire controller
        inputController.Disable();
    }


    void Start()
    {
        //Allow for detection of collisions
        charC.detectCollisions = true;

        //Set default move speed
        speed = myStats[(int)TweenVars.Walking].speed;
    }

    void Update()
    {
        //<-- Gravity -->
        velocity.y += gravity * Time.deltaTime * 2;
        charC.Move(velocity * Time.deltaTime);

        //<-- Jumping -->
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded)
        {
            //Reset Velocity
            if(velocity.y < 0)
                velocity.y = -2f;

            JumpHandler(); 
        }

        //<-- Crouching -->
        if(crouch.ReadValue<float>() > 0)
            charC.height = .9f;
        else if(charC.height != 1.8f)
            charC.height = 1.8f;

        //<-- Dashing -->
        if(dash.ReadValue<float>() > 0)
            NewDash();
        // if(isPlayerFloating)
        //     DashFloatHandler();

        //<-- Movement -->
        SpeedHandler();
        MovementHandler();
    }

    void MovementHandler()
    {
        //Grab value of movement input
        Vector2 movement = move.ReadValue<Vector2>();
        
        //Move character
        Vector3 moveVector  = transform.right * movement.x + transform.forward * movement.y;
        charC.Move(moveVector * speed * Time.deltaTime);
    }

    void SpeedHandler()
    {
        float[] targets = new float[3];

        //Handle speed based on input
        if(crouch.ReadValue<float>() > 0 && run.ReadValue<float>() > 0)
            targets = SetState(TweenVars.CrouchRun);
        else if(crouch.ReadValue<float>() > 0)
            targets = SetState(TweenVars.Crouch);
        else if(run.ReadValue<float>() > 0)
            targets = SetState(TweenVars.Run);
        else
            targets = SetState(TweenVars.Walking);

        speed = targets[0];
        float targetFOV = targets[1];
        float targetAlpha = targets[2];

        //Handle FOV tween
        if(!isDashing)
            FOVTween(tweenTime, targetFOV, targetAlpha, targetScale);
    }

    Vector3 targetScale;
    float[] SetState(TweenVars state)
    {
        float[] vals = new float[4];

        vals[0] = myStats[(int)state].speed;
        vals[1] = myStats[(int)state].fovVal;
        vals[2] = myStats[(int)state].speedLineAlpha;
        targetScale =  myStats[(int)state].speedLineScale;

        if(myStats[(int)state].headBob != null && move.ReadValue<Vector2>().y > 0)
            Camera.main.gameObject.GetComponent<Animator>().Play(myStats[(int)state].headBob.name);
        else
            Camera.main.gameObject.GetComponent<Animator>().Play("[EMPTY]");

        return vals;
    }

    void FOVTween(float time, float targetFOV, float targetSpeedLinesAlpha, Vector3 targetSpeedLinesScale)
    {
        if(currentFOV == targetFOV)
            return;

        currentFOV = targetFOV;
        fovTweener.StartFloatTween(this, Camera.main.fieldOfView, targetFOV, val => Camera.main.fieldOfView = val, time);
        alphaTweener.StartFloatTween(this, speedLines.color.a, targetSpeedLinesAlpha, val => speedLines.color = new Color(1, 1, 1, val), time);

        scaleTweener.StartVector3Tween(this, speedLines.transform.localScale, targetSpeedLinesScale, val => speedLines.transform.localScale = val, time);
        
    }

    void JumpHandler()
    {
        if(jump.ReadValue<float>() == 0)
            return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * -9.8f);
    }

    void NewDash()
    {
        if(isDashing)
            return;
        if(!canDash)
            return;

        isDashing = true;
        canDash = false;

        TweenUtils tweenUtils = new();
        Vector3 target = transform.position + (Camera.main.transform.forward * dashForce);
        tweenUtils.StartPositionTween(this, transform.position, target, val => transform.position = val, 1, dashTime);

        //Handle tweens
        float[] targets =SetState(TweenVars.Dash);
        float targetFOV = targets[1];
        float targetAlpha = targets[2];
        FOVTween(tweenTime, targetFOV, targetAlpha, targetScale);

        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        staminaBar.color = staminaUsingColor;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        
        //Reset values
        staminaBar.color = staminaDefaultColor;

        if(gravity == 0)
        {
            velocity = Camera.main.transform.forward * (dashForce/3f);
            isPlayerFloating = true;
        }
        
        //Cooldown until new dash
        TweenUtils myTweener = new();
        myTweener.StartFloatTween(this, 0, 1, (val) => staminaBar.transform.localScale  = new Vector2(val, 1), cooldownTime);
        myTweener.TweenFinished  += () => canDash = true;
    }

    void DashFloatHandler()
    {
        if(gravity == 0)
            return;

        //Handle additional velocity from dashing in zero gravity
        isPlayerFloating = false;
        velocity = Vector3.zero;
    }
}
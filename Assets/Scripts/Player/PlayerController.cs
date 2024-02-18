using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TyeUtils;

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
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float runningCrouchSpeed = 6f;
    [SerializeField] public static float gravity = -9.8f;
    [SerializeField] public static float jumpHeight = 2f;

    [Header("Tween Stats")]
    [SerializeField] float defaultFOV;
    [SerializeField] float runFOV;
    [SerializeField] float crouchRunFOV;
    [SerializeField] float tweenTime;
    float currentFOV;

    [Header("Booster Boots")]
    [SerializeField] float dashFOV;
    [SerializeField] float dashTweenTime;
    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    [SerializeField] float cooldownTime;
    [SerializeField] Color staminaDefaultColor;
    [SerializeField] Color staminaUsingColor;
    [SerializeField] Image staminaBar;

    Vector3 dashVel;
    bool canDash;
    bool isDashing;
    float staminaDefaultWidth;
    float dashTimer;

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
        staminaDefaultWidth = staminaBar.rectTransform.rect.width;
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
        speed = walkSpeed;
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
        if(isPlayerFloating)
            DashFloatHandler();

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
        float targetFOV;
        //Handle speed based on input
        if(crouch.ReadValue<float>() > 0 && run.ReadValue<float>() > 0)
        {
            speed = runningCrouchSpeed;
            targetFOV = crouchRunFOV;
        }
        else if(crouch.ReadValue<float>() > 0)
        {
            speed = crouchSpeed;
            targetFOV = defaultFOV;
        }
        else if(run.ReadValue<float>() > 0)
        {
            speed = runSpeed;
            targetFOV = runFOV;
        }
        else
        {
            speed = walkSpeed;
            targetFOV = defaultFOV;
        }

        //Handle FOV tween
        if(!isDashing)
            FOVTween(tweenTime, targetFOV);
    }

    TweenUtils fovTweener = new();
    void FOVTween(float time, float targetFOV)
    {
        if(currentFOV == targetFOV)
            return;

        currentFOV = targetFOV;
        fovTweener.StartFloatTween(this, Camera.main.fieldOfView, targetFOV, (val) => { Camera.main.fieldOfView = val; }, time);
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
        FOVTween(tweenTime, dashFOV);
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
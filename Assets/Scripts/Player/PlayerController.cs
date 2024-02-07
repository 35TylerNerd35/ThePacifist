using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("Booster Boots")]
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
            if(velocity.y < 0)
            {
                //Reset velocity
                velocity.y = -2f;
            }
            JumpHandler(); 
        }

        //<-- Crouching -->
        if(crouch.ReadValue<float>() > 0)
        {
            charC.height = .9f;
        }
        else if(charC.height != 1.8f)
        {
            charC.height = 1.8f;
        }

        //<-- Dashing -->
        if(dash.ReadValue<float>() > 0)
        {
            DashHandler();
        }
        if(!canDash && !isDashing)
        {
            DashCooldownHandler();
        }
        if(isPlayerFloating)
        {
            DashFloatHandler();
        }

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
        //Handle speed based on input
        if(crouch.ReadValue<float>() > 0 && run.ReadValue<float>() > 0)
            speed = runningCrouchSpeed;
        else if(crouch.ReadValue<float>() > 0)
            speed = crouchSpeed;
        else if(run.ReadValue<float>() > 0)
            speed = runSpeed;
        else
            speed = walkSpeed;
    }

    void JumpHandler()
    {
        if(jump.ReadValue<float>() == 0)
            return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * -9.8f);
    }

    void DashHandler()
    {
        if(!canDash && !isDashing)
            return;

        //Add velocity in direction of camera
        dashVel = Camera.main.transform.forward * dashForce;
        velocity += dashVel;
        StartCoroutine(DashCooldown());
    }

    void DashCooldownHandler()
    {
        //Set width of stamina bar depending on cooldown time
        dashTimer += Time.deltaTime;
        staminaBar.rectTransform.sizeDelta = new Vector2(staminaDefaultWidth/cooldownTime * dashTimer, staminaBar.rectTransform.rect.height);    
    }

    void DashFloatHandler()
    {
        if(gravity == 0)
            return;

        //Handle additional velocity from dashing in zero gravity
        isPlayerFloating = false;
        velocity = Vector3.zero;
    }

    IEnumerator DashCooldown()
    {
        //Set as using stamina
        staminaBar.color = staminaUsingColor;

        //Ensure correct values
        isDashing = true;
        canDash = false;
        yield return new WaitForSeconds(dashTime);
        
        //Reset values
        staminaBar.color = staminaDefaultColor;
        isDashing = false;
        velocity = Vector3.zero;
        dashVel = Vector3.zero;

        //Add momentum velocity for zero gravity
        if(gravity == 0)
        {
            velocity = Camera.main.transform.forward * (dashForce/3f);
            isPlayerFloating = true;
        }

        //Full reset after cooldown
        yield return new WaitForSeconds(cooldownTime);
        dashTimer = 0;
        canDash = true;
    }
}
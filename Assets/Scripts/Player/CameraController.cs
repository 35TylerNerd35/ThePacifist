using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public BaseInputController inputController;
    PlayerInput playerInput;
    InputAction look;

    [Header("Stats")]
    [SerializeField] Vector2 defaultSensitivity;
    [SerializeField] Vector2 sensitivity;
    [SerializeField] Slider sensSliderX;
    [SerializeField] Slider sensSliderY;

    float xRot = 0;

    [Header("References")]
    [SerializeField] Transform playerBody;
    [SerializeField] Transform playerHead;

    float speed;
    Vector3 velocity;
    Quaternion headStartRot;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        headStartRot = playerHead.localRotation;

        // UpdateSens();
    }

    void Update()
    {
        Vector2 lookController = playerInput.actions["Look"].ReadValue<Vector2>();

        //Rotate player body
        playerBody.Rotate(Vector3.up * lookController.x * sensitivity.x);;

        //Clamp x rotation
        xRot -= lookController.y * sensitivity.y;
        xRot = Mathf.Clamp(xRot, -70f, 70f);
        
        //Rotate
        playerHead.localRotation = Quaternion.Euler(headStartRot.x + xRot, playerHead.localRotation.y, playerHead.localRotation.z);
    }

    public void UpdateSens()
    {
        //Grab slider value as percentage
        float sensValMultiplierX = sensSliderX.value * 0.01f;
        float sensValMultiplierY = sensSliderY.value * 0.01f;

        //Apply percentage to sens
        sensitivity.x = defaultSensitivity.x * 2 * sensValMultiplierX;
        sensitivity.y = defaultSensitivity.y * 2 * sensValMultiplierY;
    }
}

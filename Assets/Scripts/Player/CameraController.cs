using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    PlayerInput playerInput;

    [Header("Stats")]
    [SerializeField] Vector2 defaultSensitivity;
    [SerializeField] Vector2 sensitivity;

    float xRot = 0;

    [Header("References")]
    [SerializeField] Transform playerBody;
    [SerializeField] Transform playerHead;

    float speed;
    Vector3 velocity;
    Quaternion headStartRot;

    void Awake()
    {
        LoadSettingsData.SettingsUpdated += UpdateSettings;
        UpdateSettings();

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

    public void UpdateSettings()
    {
        sensitivity.x = defaultSensitivity.x * SaveSystem.data.sensSliders[0];
        sensitivity.y = defaultSensitivity.y * SaveSystem.data.sensSliders[1];
    }
}

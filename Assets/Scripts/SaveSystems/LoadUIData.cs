using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LoadUIData : MonoBehaviour
{
    [SerializeField] bool isGameScene;
    bool isPaused;

    public BaseInputController inputController;
    InputAction pause;

    [Header("Sliders")]
    [SerializeField] Slider[] sliderVars;
    [SerializeField] string[] playerPrefSliderString;
    [SerializeField] float[] sliderDefaultVal;

    [Header("Toggles")]
    [SerializeField] Toggle[] toggleVars;
    [SerializeField] string[] playerPrefToggleString;
    [SerializeField] bool[] toggleDefaultVal;

    void Awake()
    {
        //Init new input system instance
        inputController = new BaseInputController();
    }

    void OnEnable()
    {
        //Enable controller
        inputController.Enable();

        //Enable input actions
        pause = inputController.MenuInputs.Close;
        pause.Enable();
        
        LoadData();
    }

    void OnDisable()
    {
        //Disable inputs
        pause.Disable();

        //Disable the entire controller
        inputController.Disable();

        SaveData();
    }

    void LoadData()
    {
        //Load slider data
        int currentSliderIteration = 0;
        foreach(Slider var in sliderVars)
        {
            //Set the value of the slider
            var.value = PlayerPrefs.GetFloat(playerPrefSliderString[currentSliderIteration], sliderDefaultVal[currentSliderIteration]);

            //Add to iteration count
            currentSliderIteration++;
        }

        //Load toggle data
        int currentToggleIteration = 0;
        foreach(Toggle var in toggleVars)
        {
            //Grab saved int
            int boolIntVal = PlayerPrefs.GetInt(playerPrefToggleString[currentToggleIteration], toggleDefaultVal[currentToggleIteration] ? 1: 0);

            //Set the value of the toggle (convert int to bool)
            var.isOn = boolIntVal != 0;

            if(var.GetComponent<ToggleGraphics>())
                var.GetComponent<ToggleGraphics>().ToggleValueChanged(var);

            //Add to iteration count
            currentToggleIteration++;
        }
    }
    
    public void SaveData()
    {
        //Save slider data
        int currentSliderIteration = 0;
        foreach(Slider var in sliderVars)
        {
            //Grab the value of the slider
            PlayerPrefs.SetFloat(playerPrefSliderString[currentSliderIteration], var.value);

            //Add to iteration count
            currentSliderIteration++;
        }

        //Save slider data
        int currentToggleIteration = 0;
        foreach(Toggle var in toggleVars)
        {
            //Grab bool as int
            int boolVal = var.isOn ? 1: 0;

            //Grab the value of the toggle
            PlayerPrefs.SetInt(playerPrefToggleString[currentToggleIteration], boolVal);

            //Add to iteration count
            currentToggleIteration++;
        }
        
        PlayerPrefs.Save();
    }


    //Allow Player to open Options Menu
    void Update()
    {
        if(isGameScene)
        {
            if(pause.ReadValue<float>() != 0)
            {
                //Invert bool
                isPaused = !isPaused;

                transform.GetChild(0).gameObject.SetActive(isPaused);
                SaveData();


                if(isPaused)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                } 
            }
        }
    }
}

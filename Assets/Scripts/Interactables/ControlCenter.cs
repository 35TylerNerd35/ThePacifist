using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

enum CommandState { GravSwitch, move, fall, jump }

public class ControlCenter : MonoBehaviour
{
    public static bool _isPlayerAdmin;

    //Input
    public BaseInputController inputController;
    InputAction selectAll;
    InputAction unselect;
    InputAction submit;
    InputAction backspace;
    InputAction caretMovement;

    [Header("Commands")]
    [SerializeField] string[] commands;
    [SerializeField] string[] commandResponses;
    [SerializeField] List<string> logLines;
    [SerializeField] List<string> responseLines;

    [Header("Text References")]
    [SerializeField] TMP_Text inputText;
    [SerializeField] TMP_Text inputLog;
    [SerializeField] TMP_Text responseLog;
    [SerializeField] TMP_Text caretText;

    [Header("Cmd References")]
    [SerializeField] string[] helpLines;
    [SerializeField] Sit sitScript;

    [Header("Select All")]
    [SerializeField] GameObject allSelectedBox;
    bool isAllSelected = false;

    int caretPos = 0;

//---------------------Inputs---------------------\\
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
        unselect = inputController.ConsoleInputs.Unselect;
        unselect.Enable();

        selectAll = inputController.ConsoleInputs.SelectAll;
        selectAll.Enable();

        submit = inputController.ConsoleInputs.Submit;
        submit.Enable();

        backspace = inputController.ConsoleInputs.Backspace;
        backspace.Enable();

        caretMovement = inputController.ConsoleInputs.CaretMovement;
        caretMovement.Enable();
    }
    void OnDisable()
    {
        //Disable inputs
        selectAll.Disable();
        unselect.Disable();
        submit.Disable();
        backspace.Disable();
        caretMovement.Disable();

        //Disable the entire controller
        inputController.Disable();
    }

//---------------------Run Functions---------------------\\
    void Update()
    {
        InputHandler();
        CaretHandler();
    }

//---------------------Main---------------------\\
    void CaretHandler()
    {
        //Establish local var
        string caretString = "";

        //Add previous letter(s) to temp string if it exists
        if(caretPos - 2 >= 0)
        {
            caretString += inputText.text[caretPos-2];
            caretString += inputText.text[caretPos-1];
        }
        else if(caretPos - 1 >= 0)
        {
            caretString += inputText.text[caretPos-1];
        }

        //Add caret indicator
        caretString += "|";

        //Add next letter(s) to temp string if it exists
        if(caretPos+1 < inputText.text.Length)
        {
            caretString += inputText.text[caretPos];
            caretString += inputText.text[caretPos+1];
        }
        else if(caretPos < inputText.text.Length)
        {
            caretString += inputText.text[caretPos];
        }

        //Update text
        caretText.text = caretString;
    }

    void InputHandler()
    {
        if(selectAll.ReadValue<float>() != 0)
        {
            SelectAll(true);
        }
        else if(unselect.ReadValue<float>() != 0 && isAllSelected)
        {
            SelectAll(false);
        }
        else if(submit.ReadValue<float>() != 0)
        {
            SubmitText(inputText.text, logLines, inputLog, 6);
            SelectAll(false);
        }
        else if(backspace.ReadValue<float>() != 0 && caretPos > 0)
        {
            if(!isAllSelected)
            {
                //Remove from end of input
                string firstString = inputText.text.Substring(0, caretPos - 1);
                string secondString = inputText.text.Substring(caretPos);
                inputText.text = firstString + secondString;

                //Ensure caret position is corrected
                caretPos --;
            }
            else
            {
                //Reset input
                inputText.text = "";
                caretPos = 0;
                SelectAll(false);
            }            
        }
        else if(caretMovement.ReadValue<float>() != 0)
        {
            float moveDir = caretMovement.ReadValue<float>();
            if(isAllSelected)
            {
                //Place caret at start or end of text
                if(moveDir < 0)
                {
                    caretPos = 0;
                }
                else
                {
                    caretPos = inputText.text.Length;
                }

                //Unselect
                SelectAll(false);
            }
            else
            {
                //If able, move caret one left or right
                if(moveDir < 0 && caretPos > 0)
                {
                    caretPos -= 1;
                }
                else if(moveDir > 0 && caretPos < inputText.text.Length)
                {
                    caretPos += 1;
                }
            }
        }
        else
        {
            if(isAllSelected)
            {
                string newText = "";

                //Grab temp reference to player's keystrokes (omitting backspace)
                if(!Input.GetKey(KeyCode.Backspace))
                {
                    newText = Input.inputString; 
                }
                
                if(newText != "")
                {
                    //Reset input
                    inputText.text = newText;
                    caretPos = 1;
                
                    //Unselect text
                    SelectAll(false);
                }
            }
            else
            {
                //Ommit backspaces
                if(!Input.GetKey(KeyCode.Backspace))
                {
                    if(caretPos == inputText.text.Length)
                    {
                        //If caret at end of line, add text to string
                        inputText.text = inputText.text + Input.inputString;
                        caretPos += Input.inputString.Length;
                    }
                    else
                    {
                        //If caret not at end of line, add text to caret pos
                        inputText.text = inputText.text.Insert(caretPos, Input.inputString);
                        caretPos += Input.inputString.Length;
                    }
                }
            }
        }
    }

    void SelectAll(bool _selectState)
    {
        //Enable & disable select graphics & state based on paramater
        isAllSelected = _selectState;
        allSelectedBox.SetActive(_selectState);
    }

    void SubmitText(string submittedText, List<string> list, TMP_Text log, int logNumber)
    {
        //Reset input
        string tempText = "";
        inputText.text = "";
        caretPos = 0;

        //Add line break before text if not first line
        if(list.Count != 0)
        {
            tempText += "<br>";
        }
        
        //Add paramater text to string, and add it to text log, as well as logged list
        tempText += submittedText;
        log.text += tempText;
        list.Add(tempText);
        
        //If more lines than is permitted
        if(list.Count > logNumber)
        {
            //Remove overflow text
            log.text = log.text.Substring(list[0].Length);
            list.RemoveAt(0);
        }

        //If submitted string is input, attempt to run command
        if(log == inputLog)
        {
            StartCommand(submittedText.ToLower());
        }
    }

    void StartCommand(string cmdTxt)
    {
        //Declare local vars
        string functionName = "";
        int responses = 0;

        //For each command
        for(int i = 0; i < commands.Length; i++)
        {
            if(cmdTxt.Contains(commands[i]))
            {
                //Submit response string to log
                SubmitText(commandResponses[i], responseLines, responseLog, 11);

                //Append to local vars
                responses++;
                functionName = commands[i];
            }
        }

        
        if(responses == 0)
        {
            SubmitText("<color=red>WARNING: </color><color=yellow>Command not found </color>", responseLines, responseLog, 11);
            return;
        }

        transform.SendMessage("Console" + functionName[..1].ToUpper() + functionName.Substring(1).ToLower());
    }
    

    //---------------------Console Commands---------------------\\

    void ConsoleQuit()
    {
        sitScript.isClosing = true;
    }

    void ConsoleHelp()
    {
        //Cycle through and print all help commands
        for(int i = 0; i < helpLines.Length; i++)
        {
            SubmitText(helpLines[i], responseLines, responseLog, 11);
        }
    }

    void ConsoleGrav()
    {
        //Add state of gravity to log (same line)
        responseLog.text += " to " + GravityCmd.isGravOn;
    }

    void ConsoleTest() => SubmitText("---------------------", responseLines, responseLog, 11);

    void ConsoleMakemeadmin()
    {
        _isPlayerAdmin = true;
    }
}
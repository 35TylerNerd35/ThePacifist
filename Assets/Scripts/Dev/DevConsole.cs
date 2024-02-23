using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevConsole : MonoBehaviour
{
    [SerializeField] GameObject consoleContainer;
    [SerializeField] GameObject consoleContainerBack;
    bool isConsoleOpen;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Slash) && !isConsoleOpen)
        {
            isConsoleOpen = true;
            consoleContainer.GetComponent<Animator>().Play("In");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Close()
    {
        isConsoleOpen = false;
        consoleContainer.GetComponent<Animator>().Play("Out");
        Cursor.lockState = CursorLockMode.Locked;
    }
}

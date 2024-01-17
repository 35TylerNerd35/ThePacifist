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
            consoleContainer.SetActive(true);
            consoleContainerBack.SetActive(true);
        }
    }
}

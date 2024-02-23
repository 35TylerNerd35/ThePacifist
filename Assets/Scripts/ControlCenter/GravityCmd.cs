using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GravityCmd : MonoBehaviour
{
    public static bool isGravOn;

    public void ConsoleGrav()
    {
        float gravity = PlayerController.gravity;

        if(gravity != -9.8f)
        {
            //Set player gravity
            PlayerController.gravity = -9.8f;
            PlayerController.jumpHeight = 2f;

            //Stop gravity randomiser
            isGravOn = false;

            Camera.main.GetComponent<Animator>().enabled = true;
        }
        else
        {
            isGravOn = true;

            PlayerController.gravity = 0;
            PlayerController.jumpHeight = .5f;

            Camera.main.GetComponent<Animator>().enabled = false;
        }
    }
}

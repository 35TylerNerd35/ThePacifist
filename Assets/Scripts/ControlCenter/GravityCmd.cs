using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GravityCmd : MonoBehaviour
{
    public static bool isGravOn;
    bool wasAnimatorOn;

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
        }
        else
        {
            isGravOn = true;

            PlayerController.gravity = 0;
            PlayerController.jumpHeight = .5f;
        }
    }
}

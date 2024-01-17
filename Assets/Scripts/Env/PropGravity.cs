using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGravity : MonoBehaviour
{
    bool isRunningGravity;
    float randomGrav;

    void Awake()
    {
        isRunningGravity = false;
    }

    void Update()
    {
        if(GravityCmd.isGravOn && !isRunningGravity)
        {
            StartCoroutine(RandomiseGrav());
        }
        else if(!GravityCmd.isGravOn)
        {
            //Stop gravity randomiser
            StopAllCoroutines();

            //Set game gravity
            Physics.gravity = new Vector3(0, -9.8f, 0);

            //Set grav state
            isRunningGravity = false;
        }
    }

    IEnumerator RandomiseGrav()
    {
        if(!isRunningGravity)
        {
            isRunningGravity = true;
        }

        //Grab random gravity value
        if(randomGrav > 0)
        {
            randomGrav = Random.Range(-.1f, -.15f);
        }
        else if(randomGrav < 0)
        {
            randomGrav = Random.Range(.1f, .15f);
        }
        else
        {
            randomGrav = Random.Range(.05f, .25f);
        }
        

        //Set global gravity of rbs
        Physics.gravity = new Vector3(0, randomGrav, 0);

        //Wait before new grav
        yield return new WaitForSeconds(Random.Range(.125f, .15f));
        StartCoroutine(RandomiseGrav());
    }
}

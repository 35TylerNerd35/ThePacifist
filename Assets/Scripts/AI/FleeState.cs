using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : StateBaseClass
{
    float runSpeedMultiplier = 3f;

    public override void StartMyState()
    {
        myManager.agent.speed *= runSpeedMultiplier;
        myManager.SetDestination(new Vector3(2, 4, 2));
    }

    public override void EndMyState()
    {
        myManager.agent.speed /= runSpeedMultiplier;
        Debug.Log("End RUNNING");
    }

    public override void UpdateMyState()
    {
        
    }
}

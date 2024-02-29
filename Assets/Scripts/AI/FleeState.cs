using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : StateBaseClass
{
    public override void StartMyState()
    {
        Debug.Log("Start RUNNING");
        targetPos = new(2, 4, 2);
    }

    public override void EndMyState()
    {
        Debug.Log("End RUNNING");
    }

    public override void UpdateMyState()
    {
        
    }
}

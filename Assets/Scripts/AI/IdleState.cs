using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBaseClass
{
    public override void StartMyState()
    {
        Debug.Log("Start Idle");
    }

    public override void EndMyState()
    {
        Debug.Log("End Idle");
    }

    public override void UpdateMyState()
    {
        
    }
}

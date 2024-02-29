using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateBaseClass
{
    public override void StartMyState()
    {
        Debug.Log("Start Patrol");
        targetPos = new(2, 4, 2);
    }

    public override void EndMyState()
    {
        Debug.Log("End Patrol");
    }

    public override void UpdateMyState()
    {
        
    }
}

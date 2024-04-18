using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateBaseClass
{
    public override void StartMyState()
    {
        Debug.Log("Start FOLLOWING");
        myManager.SetDestination(new Vector3(2, 4, 2));
    }

    public override void EndMyState()
    {
        Debug.Log("End FOLLOWING");
    }

    public override void UpdateMyState()
    {
        
    }
}

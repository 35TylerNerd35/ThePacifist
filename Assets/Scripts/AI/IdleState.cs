using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBaseClass
{
    int chanceToPatrol = 15;

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
        if(FollowCheck())
            myManager.SwitchState(States.Follow);
        else if(PatrolCheck())
            myManager.SwitchState(States.Patrol);
    }

    bool PatrolCheck()
    {
        return Random.Range(0, 100) < chanceToPatrol;
    }

    bool FollowCheck()
    {
        return false;
    }
}

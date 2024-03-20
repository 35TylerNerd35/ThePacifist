using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBaseClass
{
    int chanceToPatrol = 15;
    bool canPatrol;

    public override void StartMyState()
    {
        targetPos = transform.parent.position;
        StartCoroutine(AllowPatrol());

        GameLog.Log(0, this.ToString(), "Start Idle State");
    }

    public override void EndMyState()
    {
        GameLog.Log(2, this.ToString(), "End Idle State");
    }

    public override void UpdateMyState()
    {
        if(FollowCheck())
        {
            myManager.SwitchState(States.Follow);
        }
        else if(PatrolCheck())
        {
            myManager.SwitchState(States.Patrol);
        }
    }

    IEnumerator AllowPatrol()
    {
        yield return new WaitForSeconds(3);
        canPatrol = true;
    }

    bool PatrolCheck()
    {
        if(!canPatrol)
            return false;

        return Random.Range(0, 100) < chanceToPatrol;
    }

    bool FollowCheck()
    {
        return false;
    }
}

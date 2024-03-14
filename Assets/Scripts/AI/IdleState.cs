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
        PatrolSwitch();
    }

    void PatrolSwitch()
    {
        if(Random.Range(0, 100) > 15)
            return;

        GetComponent<StateManager>().SwitchState(States.Patrol);
    }
}

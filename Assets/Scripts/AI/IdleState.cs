using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBaseClass
{
    int chanceToPatrol = 15;
    bool canPatrol;

    int myIdleIndex = 0;

    public override void StartMyState()
    {
        targetPos = transform.parent.position + (transform.parent.forward * .2f);
        canPatrol = false;
        StartCoroutine(CheckPatrol());
        StartCoroutine(ChangeIdle());

        IEnumerator CheckPatrol()
        {
            while(isRunning)
            {
                yield return new WaitForSeconds(Random.Range(1.3f, 6));
                canPatrol = Random.Range(0, 100) < chanceToPatrol;
            }
        }

        IEnumerator ChangeIdle()
        {
            while(isRunning)
            {
                yield return new WaitForSeconds(Random.Range(1, 4));
                myIdleIndex += 1;

                if(myIdleIndex >= 5)
                    myIdleIndex = 0;

                myManager.AnimationSwitch($"Idle {myIdleIndex}");
            }
        }
    }

    public override void EndMyState()
    {
        StopAllCoroutines();
    }

    public override void UpdateMyState()
    {
        if(canPatrol)
        {
            myManager.SwitchState(States.Patrol);
        }
    }
}

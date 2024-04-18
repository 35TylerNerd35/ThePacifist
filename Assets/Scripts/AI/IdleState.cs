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
        Vector3 targetPos = transform.parent.position + (transform.parent.forward * .2f);
        myManager.SetDestination(targetPos);

        canPatrol = false;
        StartCoroutine(CheckPatrol());
        StartCoroutine(ChangeIdle());

        IEnumerator CheckPatrol()
        {
            while(isRunning)
            {
                yield return new WaitForSeconds(Random.Range(1.3f, 3));
                canPatrol = Random.Range(0, 100) < chanceToPatrol;
            }
        }

        IEnumerator ChangeIdle()
        {
            while(isRunning)
            {
                yield return new WaitForSeconds(Random.Range(1, 4));

                myIdleIndex = Random.Range(0, 5);

                myManager.AnimationSwitch($"Idle {myIdleIndex}", .4f);
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
            // GameLog.Log(this.ToString(), "Switching to Patrol...", 0);
            myManager.SwitchState(States.Patrol);
        }
    }
}

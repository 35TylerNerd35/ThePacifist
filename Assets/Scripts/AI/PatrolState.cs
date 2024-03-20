using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateBaseClass
{
    bool checkPatrolFinished;

    public override void StartMyState()
    {
        targetPos = RandPatrolPoint();
        GameLog.Log(1, this.ToString(), "Start Patrol State");
        StartCoroutine(CheckPatrol());
    }

    public override void EndMyState()
    {
        checkPatrolFinished = false;
    }

    public override void UpdateMyState()
    {
        if(!checkPatrolFinished)
            return;

        if(PatrolFinished())
            myManager.SwitchState(States.Idle);
    }

    IEnumerator CheckPatrol()
    {
        yield return new WaitForSeconds(1);
        checkPatrolFinished = true;
    }

    bool PatrolFinished()
    {
        float totalVel = myManager.agent.velocity.x + myManager.agent.velocity.y + myManager.agent.velocity.z;

        return totalVel < 1;
    }

    Vector3 RandPatrolPoint()
    {
        float multiplier = transform.parent.parent.GetChild(0).localScale.x;
        //Find random point within range and add to current position to get new pos
        Vector3 randDir = Random.insideUnitSphere * multiplier;
        randDir += transform.parent.parent.position;

        //Find closest point on nav mesh
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randDir, out hit, multiplier, UnityEngine.AI.NavMesh.AllAreas);

        return hit.position;
    }
}

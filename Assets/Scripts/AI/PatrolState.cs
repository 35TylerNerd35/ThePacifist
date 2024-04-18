using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateBaseClass
{
    bool canStop;

    public override void StartMyState()
    {
        myManager.SetDestination(RandPatrolPoint());

        StartCoroutine(AllowStop());
    }

    public override void EndMyState()
    {
        StopAllCoroutines();
        canStop = false;
    }

    public override void UpdateMyState()
    {
        if(!canStop)
            return;

        if(PatrolFinished())
            myManager.SwitchState(States.Idle);
    }

    IEnumerator AllowStop()
    {
        yield return new WaitForSeconds(1);
        canStop = true;
    }

    bool PatrolFinished()
    {
        float totalVel = myManager.agent.velocity.x + myManager.agent.velocity.y + myManager.agent.velocity.z;

        return totalVel < 1;
    }

    Vector3 RandPatrolPoint()
    {
        Vector3 returnVal;

        float multiplier = transform.parent.parent.GetChild(0).localScale.x;
        //Find random point within range and add to current position to get new pos
        Vector3 randDir = Random.insideUnitSphere * multiplier;
        randDir += transform.parent.parent.position;

        //Find closest point on nav mesh
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randDir, out hit, multiplier, UnityEngine.AI.NavMesh.AllAreas);

        if(Vector3.Distance(hit.position, transform.parent.position) < 10)
            returnVal = RandPatrolPoint();
        else
            returnVal = hit.position;

        return returnVal;
    }
}

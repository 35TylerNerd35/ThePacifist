using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBaseClass
{
    public override void StartMyState()
    {
        Debug.Log("Start Attack");
        myManager.SetDestination(new Vector3(2, 4, 2));
    }

    public override void EndMyState()
    {
        Debug.Log("End Attack");
    }

    public override void UpdateMyState()
    {
        
    }
}

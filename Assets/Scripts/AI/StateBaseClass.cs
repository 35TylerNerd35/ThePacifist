using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateBaseClass : MonoBehaviour
{
    bool isRunning;
    
    protected NavMeshAgent agent;
    protected StateManager myManager;
    protected Vector3 targetPos;
    protected Animator anim;

    [SerializeField] string clipToPlay;

    public abstract void StartMyState();
    public abstract void UpdateMyState();
    public abstract void EndMyState();

    void Awake()
    {
        //Grab components
        if(!GetComponent<StateManager>())
            return;

        myManager = GetComponent<StateManager>();
        agent = myManager.agent;
        anim = transform.parent.GetComponent<Animator>();
    }

    public void StartState()
    {
        isRunning = true;
        anim.SetTrigger(clipToPlay);

        StartMyState();
    }

    public void EndState()
    {
        isRunning = false;
        EndMyState();
    }

    void Update(){
        if(isRunning)
            UpdateState();
    }

    public void UpdateState()
    {
        if(agent != null)
            agent.destination = targetPos;

        if(CheckAttack())
            myManager.SwitchState(States.Attack);

        if(CheckFollow())
            myManager.SwitchState(States.Follow);

        UpdateMyState();
    }

    bool CheckAttack()
    {
        return false;
    }

    bool CheckFollow()
    {
        return false;
    }

}

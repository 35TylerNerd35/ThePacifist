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
    }

    public void StartState()
    {
        isRunning = true;

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

        UpdateMyState();
    }

}

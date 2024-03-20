using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public enum States{
    Idle,
    Patrol,
    Follow,
    Attack,
    Flee
}

public class StateManager : MonoBehaviour
{
    [Header("My States")]
    [SerializeField] States initState;
    [SerializeField] States currentState;
    [SerializeField] StateBaseClass[] states;
    States lastState;
    
    [HideInInspector] public NavMeshAgent agent;

    void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();

        //Setup init state
        states[(int)initState].StartState();
        currentState = initState;
        lastState = initState;
    }

    void Update()
    {
        if(currentState != lastState)
            SwitchState(currentState);
    }

    public void SwitchState(States newState)
    {
        states[(int)lastState].EndState();
        lastState = newState;
        states[(int)lastState].StartState();
    }
}

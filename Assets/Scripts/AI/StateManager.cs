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
    [HideInInspector] public Animator animator;

    string currentAnim;

    void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();

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
        GameLog.Log(this.ToString(), $"Ending {lastState} State", 2);
        lastState = newState;
        currentState = newState;
        states[(int)lastState].StartState();
        GameLog.Log(this.ToString(), $"Starting {lastState} State");
    }

    public void AnimationSwitch(string anim, float transitionTime = .2f)
    {
        if(currentAnim == anim)
            return;

        animator.CrossFade(anim, transitionTime);
    }
}

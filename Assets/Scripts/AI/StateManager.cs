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
    [HideInInspector] public List<GameObject> myEnemyList;
    [Space]
    public string tagToAttack;
    string currentAnim;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();

        currentHealth = maxHealth;

        //Setup init state
        states[(int)initState].StartState();
        currentState = initState;
        lastState = initState;
    }

    void OnEnable()
    {
        InitialCheckForEnemies();
    }

    void Update()
    {
        if(currentState != lastState)
            SwitchState(currentState);

        if(currentHealth < (maxHealth / 5))
            currentState = States.Flee;
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

    void InitialCheckForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 300, 6);

        foreach (Collider col in colliders) //add all enemies found in an initial list
        {
            if (Vector3.Distance(transform.position, col.transform.position) < 1)
                return;

            if(col.gameObject.tag != "Player" && col.gameObject.tag != tagToAttack)
                return;

            myEnemyList.Add(col.gameObject);
        }
    }
}

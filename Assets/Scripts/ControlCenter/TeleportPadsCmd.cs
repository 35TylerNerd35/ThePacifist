using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleportPadsCmd : MonoBehaviour
{
    [SerializeField] float teleportActivationTime = 3f;
    [SerializeField] float teleportDelayTime = 5f;
    // [SerializeField] float teleportCooldownTime = 10f;

    public static bool isUnlocked;
    public static bool isActivated;
    public static Transform activatedConsole;
    float activationTime;

    void Awake()
    {
        isUnlocked = true;
    }

    void ConsoleTele()
    {
        if(!isUnlocked)
            return;

        //Set lock state of function
        isUnlocked = false;
        StartCoroutine(StartUnlockSequence());

        //Start teleportation sequence
        activationTime = 0;
        activatedConsole = transform;
        isActivated = true;
    }

    IEnumerator StartUnlockSequence()
    {
        yield return new WaitForSeconds(teleportDelayTime);
        isUnlocked = true;
    }

    void Update()
    {
        if(activatedConsole != transform)
            return;

        if(!isActivated)
            return;

        Timer();
    }

    void Timer()
    {
        activationTime += Time.deltaTime;

        //End time
        if(activationTime < teleportActivationTime)
            return;
        
        isActivated = false;
        activatedConsole = null;
    }
}

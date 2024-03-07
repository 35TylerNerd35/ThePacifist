using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TyeUtils;

public class TweenActivate : MonoBehaviour
{
    [SerializeField] GameObject[] active;
    [SerializeField] bool[] activeStates;
    [Space]
    [SerializeField] float tweenTime;
    [SerializeField] Transform target;
    [SerializeField] Transform targetDest;
    [Space]
    [SerializeField] bool destroyOnFinish;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
            return;

        TweenUtils tween = new();
        tween.TweenFinished += Callback;

        tween.StartVector3Tween(this, target.position, targetDest.position, val => target.position = val, tweenTime);
    }

    public void Callback()
    {
        for(int i = 0; i < activeStates.Length; i++)
        {
            active[i].SetActive(activeStates[i]);
        }

        if(destroyOnFinish)
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    [SerializeField] bool isOnEnable;
    [SerializeField] Animator anim;

    void OnEnable()
    {
        if (isOnEnable)
        {
            anim.enabled = false;
        }
    }
}

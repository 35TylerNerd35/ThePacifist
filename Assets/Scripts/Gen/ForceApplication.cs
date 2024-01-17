using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplication : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 force;

    void OnEnable()
    {
        rb.AddForce(force, ForceMode.Impulse);
    }
}

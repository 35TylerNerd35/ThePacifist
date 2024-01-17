using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        Vector3 targetPos = target.position;

        transform.parent.position = targetPos;
        target.position = targetPos;
    }
}

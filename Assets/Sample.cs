using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TyeUtils;

public class Sample : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    void Start()
    {
        TweenUtils utils = new();
        utils.StartPositionTween(this, transform.position, targetPos.position, (val) => transform.position = val, 1);
        // utils.StartVector3Tween(this, transform.position, targetPos.position, (val) => transform.position = val);
    }
}

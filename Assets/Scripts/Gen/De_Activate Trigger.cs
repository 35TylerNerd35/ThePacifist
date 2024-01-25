using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class De_ActivateTrigger : MonoBehaviour
{
    [SerializeField] bool _activeState = true;
    [SerializeField] bool _destroySelf = true;
    [SerializeField] GameObject obj;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
            return;

        obj.SetActive(_activeState);

        if(_destroySelf)
            Destroy(gameObject);
    }
}

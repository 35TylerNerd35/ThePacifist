using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPads : MonoBehaviour
{
    [SerializeField] float consoleRadius = 20f;
    [SerializeField] float padRadius = 1f;

    bool isCmdActivated;
    bool isPadActivated;

    void Update()
    {
        isCmdActivated = TeleportPadsCmd.isActivated;

        if(isCmdActivated && !isPadActivated)
        {
            //Check how close pad is to console
            float dist = Vector3.Distance(transform.position, TeleportPadsCmd.activatedConsole.position);

            //Activate teleport pad if close enough to console
            if(dist < consoleRadius)
            {
                isPadActivated = true;
            }
        }

        if(isPadActivated)
        {
            CheckForEnemies();
        }
    }
    
    void CheckForEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, padRadius);

        foreach(Collider collider in hitColliders)
        {
            //Grab gameobject
            GameObject obj = collider.gameObject;

            //Check if object is able to be teleported
            if(obj.GetComponent<TeleportActive>() != null)
            {
                obj.GetComponent<TeleportActive>().enabled = true;
            }
        }
    }
}

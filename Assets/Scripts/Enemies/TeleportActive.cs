using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportActive : MonoBehaviour
{
    [SerializeField] Material teleportMat;
    [SerializeField] float shaderTimeMultiplier = 0.12f;
    bool isTeleporting = false;
    float timer = 0;
    float tpDelay = 1.1f;
    

    void OnEnable()
    {
        StartCoroutine(TeleportDelay());
    }

    void Update()
    {
        if(isTeleporting)
        {
            timer += Time.deltaTime;

            teleportMat.SetFloat("_DissolveAmount", timer * shaderTimeMultiplier);

            float dissolveAmount = timer * shaderTimeMultiplier;

            if(dissolveAmount >= .98f)
            {
                Destroy(gameObject);
            }
            else if(dissolveAmount >= .35f)
            {
                transform.GetComponent<Collider>().enabled = false;
            }
        }
    }

    IEnumerator TeleportDelay()
    {
        yield return new WaitForSeconds(tpDelay);
        
        //Set to teleport material
        GetComponent<MeshRenderer>().material = teleportMat;

        //Reset teleport shader
        teleportMat.SetFloat("_DissolveAmount", 0f);

        isTeleporting = true;
    }
}

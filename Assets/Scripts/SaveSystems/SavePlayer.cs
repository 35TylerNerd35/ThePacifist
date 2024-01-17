using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class SavePlayer : MonoBehaviour
{
    [SerializeField] int saveIndex;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(PlayerPrefs.GetInt("SaveIndex") < saveIndex || !PlayerPrefs.HasKey("SaveIndex"))
            {
               PlayerPrefs.SetInt("SaveIndex", saveIndex); 
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

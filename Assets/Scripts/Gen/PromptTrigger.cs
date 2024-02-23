using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptTrigger : MonoBehaviour
{
    [SerializeField] TMP_Text promptText;
    [SerializeField] Animator anim;
    [Space]
    [SerializeField] [TextAreaAttribute]string myText;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
            return;

        promptText.text = myText;
        anim.Play("PromptIn");
    }
}

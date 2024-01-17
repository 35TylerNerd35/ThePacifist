using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Narrator : MonoBehaviour
{
    [SerializeField] string[] subtitles;
    [SerializeField] AudioClip[] voiceLines;

    [SerializeField] GameObject subtitleAnim;
    [SerializeField] TMP_Text subtitleText;

    public static int narratorIteration;
    public static bool narratorTrigger;

    void Update()
    {
        if(narratorTrigger)
        {
            //Disable trigger
            narratorTrigger = false;
            
            //Set text
            subtitleText.text = subtitles[narratorIteration];

            //Play Audio clip
            GetComponent<AudioSource>().clip = voiceLines[narratorIteration];
            GetComponent<AudioSource>().Play();
        }
    }
}

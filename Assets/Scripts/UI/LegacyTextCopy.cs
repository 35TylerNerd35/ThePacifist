using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LegacyTextCopy : MonoBehaviour
{
    [SerializeField] Text textToCopy;
    [SerializeField] TMP_Text TMPTextToCopy;
    [SerializeField] bool isTmp;
    TMP_Text thisText;

    void Awake()
    {
        thisText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(isTmp)
        {
            TMPTextToCopy.text = thisText.text;
        }
        else
        {
            if(textToCopy.text == thisText.text)
                        return;

            string text = textToCopy.text;

            if(text.Contains("Right"))
                text = text.Replace("Right ", "R.");
            if(text.Contains("Left"))
                text = text.Replace("Left ", "L.");

            if(text.Contains("Control"))
                text = text.Replace("Control", "Ctrl");

            thisText.text = text;
        }

        
    }
}
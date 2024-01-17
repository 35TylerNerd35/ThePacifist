using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] GameObject tutorialText;
    [SerializeField] string titleText = "--Incoming Transmission--";
    [SerializeField] string messageText;
    [Header("Values")]
    [SerializeField] float easeInSpeed = 500f;
    [SerializeField] float easeOutSpeed = 500f;
    [Space]
    [SerializeField] Vector2 easeInPos = new Vector2(0, -400f);
    [SerializeField] Vector2 easeOutPos = new Vector2(0, -700f);

    bool isEaseIn;
    bool isEaseOut;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isEaseOut = false;
            isEaseIn = true;

            //Update text
            tutorialText.transform.GetChild(0).GetComponent<TMP_Text>().text = titleText;
            tutorialText.transform.GetChild(1).GetComponent<TMP_Text>().text = messageText;
            tutorialText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isEaseIn = false;
            isEaseOut = true;
        }
    }

    void Update()
    {
        if(isEaseIn)
        {
            AnimateText(easeInSpeed, easeInPos);
            CheckEasePos(easeInPos);
        }
        else if(isEaseOut)
        {
            AnimateText(easeOutSpeed, easeOutPos);
            CheckEasePos(easeOutPos);
        }
    }

    void CheckEasePos(Vector2 targetPos)
    {
        if(Vector2.Distance(tutorialText.transform.localPosition, targetPos) < 1)
        {
            if(isEaseOut)
            {
                tutorialText.SetActive(false);
            }
            
            //Reset variables
            isEaseIn = false;
            isEaseOut = false;
        }
    }

    void AnimateText(float animSpeed, Vector2 targetPos)
    {
        //Grab direction to target
        Vector2 moveDist = targetPos - (Vector2)tutorialText.transform.localPosition;

        //Normalise direction (between 0 and 1)
        moveDist = moveDist.normalized;

        //Move in direction according to speed
        tutorialText.transform.localPosition += (Vector3)(moveDist * animSpeed * Time.deltaTime);
    }
}

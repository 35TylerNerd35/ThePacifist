using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    [Header("Tab Switching")]
    [SerializeField] GameObject[] tabs;
    [SerializeField] float easeSpeed;
    [SerializeField] Vector3[] targetScale;

    bool isEasingIn;
    bool isEasingOut;
    Transform tabToAnim;
    bool doAnim;

    void OnEnable()
    {
        doAnim = false;

        //Set tab to gameplay
        TabSwitch(tabs[0]);
    }
    

    public void TabSwitch(GameObject activateTab)
    {
        if(isEasingIn || isEasingOut)
            return;

        //Hide all tabs
        foreach(GameObject tab in tabs)
        {
            tab.SetActive(false);
        }

        if(doAnim)
        {
            tabToAnim = activateTab.transform.GetChild(0);
            isEasingOut = true;
        }
        else
            doAnim = true;

        //Display selected tab
        activateTab.SetActive(true); 
    }

    void Update()
    {
        if(isEasingOut)
        {
            float speed = easeSpeed * Time.deltaTime;

            tabToAnim.transform.localScale += -Vector3.one * speed;

            if(Vector3.Distance(tabToAnim.transform.localScale, targetScale[0]) < .01f)
            {
                isEasingIn = true;
                isEasingOut = false;
                speed = 0;
            }
        }
        else if(isEasingIn)
        {
            float speed = easeSpeed * Time.deltaTime;

            tabToAnim.transform.localScale += Vector3.one * speed;

            if(Vector3.Distance(tabToAnim.transform.localScale, targetScale[1]) < 0.008f)
            {
                tabToAnim.transform.localScale = targetScale[1];
                isEasingIn = false;
                speed = 0;
            }
        }
    }
}

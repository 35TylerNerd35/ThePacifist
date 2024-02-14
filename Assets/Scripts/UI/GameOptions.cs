using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] Animator swipeAnimator;
    [SerializeField] Animator fadeAnimator;
    [Header("Tab Switching")]
    [SerializeField] GameObject[] tabs;
    [SerializeField] Color indicatorColorOriginal;
    [SerializeField] Color indicatorColorSelected;
    [SerializeField] Image[] indicators;

    void OnEnable()
    {
        TabSwitch(0);
    }


    public void Disable(Animator anim)
    {
        swipeAnimator.Play("Hide");
        fadeAnimator.Play("Hide");

        StartCoroutine(DisableOptions());
    }

    IEnumerator DisableOptions()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    

    public void TabSwitch(int activateTab)
    {
        //Hide all tabs
        foreach(GameObject tab in tabs)
            tab.SetActive(false);

        //Reset all indicators
        foreach(Image indicator in indicators)
            indicator.color = indicatorColorOriginal;

        //Display selected tab
        tabs[activateTab].SetActive(true); 
        indicators[activateTab].color = indicatorColorSelected;
    }
}

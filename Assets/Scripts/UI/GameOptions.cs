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
    [Header("Values")]
    [SerializeField] Toggle fullscreenToggle;

    void Awake()
    {
        bool temp = PlayerPrefs.GetInt("isFullscreen", 1) == 1;
        fullscreenToggle.isOn = temp;
        fullscreenToggle.onValueChanged.AddListener(FullScreenController);
    }

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
        Debug.Log("WAIT");
        yield return new WaitForSeconds(1f);
        Debug.Log("DE");
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

    public void FullScreenController(bool toggleVal)
    {
        Screen.fullScreen = toggleVal;
        PlayerPrefs.SetInt("isFullScreen", toggleVal ? 1 : 0);
        PlayerPrefs.Save();
    }
}

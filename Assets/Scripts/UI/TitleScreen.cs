using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [Header("Load Scene")]
    [SerializeField] GameObject loadScreen;
    [SerializeField] Image loadBar;
    [Header("Title Anim")]
    [SerializeField] Transform titleObj;
    [Space]
    [SerializeField] float rotSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float animTime;
    [Space]
    [SerializeField] Vector3 initScale;
    [SerializeField] Vector3 zoomScale;
    bool isScalingForwards;

    float currentAnimTime;
    bool isAnimForwards;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(LoadSceneAsync(1));
    }

    IEnumerator LoadSceneAsync(int SceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        loadScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressTime = Mathf.Clamp01(operation.progress / 0.9f);
            loadBar.fillAmount = progressTime;

            yield return null;
        }
    }

    void Update()
    {
        currentAnimTime += Time.deltaTime;

        if(currentAnimTime >= animTime)
        {
            //Reset timer
            currentAnimTime = 0;

            //Change anim state
            isAnimForwards = !isAnimForwards;
        }

        ScaleTitle();
    }

    void ScaleTitle()
    {
        if(isScalingForwards)
        {
            //Zoom in title
            titleObj.localScale = Vector3.Lerp (titleObj.localScale, zoomScale, zoomSpeed * Time.deltaTime);

            //Check if target scale is reached
            if(Vector3.Distance(titleObj.localScale, zoomScale) < .01f)
            {
                isScalingForwards = !isScalingForwards;
            }
        }
        else
        {
            //Zoom out title
            titleObj.localScale = Vector3.Lerp (titleObj.localScale, initScale, zoomSpeed * Time.deltaTime);

            //Check if target scale is reached
            if(Vector3.Distance(titleObj.localScale, initScale) < .01f)
            {
                isScalingForwards = !isScalingForwards;
            }
        }
    }
}

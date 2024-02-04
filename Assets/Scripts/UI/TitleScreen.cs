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
    [SerializeField] Animator promptAnim;

    public void StartGame() => StartCoroutine(LoadSceneAsync(1));

    IEnumerator LoadSceneAsync(int SceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        loadScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressTime = Mathf.Clamp01(operation.progress / 1f);
            loadBar.fillAmount = progressTime;

            yield return null;
        }
    }
    
    public void QuitGame() => Application.Quit();
    public void QuitPromptIn() => promptAnim.Play("QuitPromptIn");
    public void QuitPromptOut() => promptAnim.Play("QuitPromptOut");
}

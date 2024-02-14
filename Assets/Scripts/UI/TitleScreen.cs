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

    void Awake() => Cursor.lockState = CursorLockMode.None;

    public void StartGame() => StartCoroutine(LoadSceneAsync(1));

    IEnumerator LoadSceneAsync(int SceneID)
    {
        loadScreen.SetActive(true);
        float initScaleX = loadBar.transform.localScale.x;
        loadBar.transform.localScale = new Vector3(0, loadBar.transform.localScale.y, loadBar.transform.localScale.z);
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);

        while(!operation.isDone)
        {
            Debug.Log(operation.progress);
            float progressTime = initScaleX * operation.progress;
            loadBar.transform.localScale = new Vector3(progressTime, loadBar.transform.localScale.y, loadBar.transform.localScale.z);

            yield return null;
        }
    }
    
    public void QuitGame() => Application.Quit();
    public void QuitPromptIn() => promptAnim.Play("QuitPromptIn");
    public void QuitPromptOut() => promptAnim.Play("QuitPromptOut");
}

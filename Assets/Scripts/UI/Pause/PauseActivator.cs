using UnityEngine;

public class PauseActivator : MonoBehaviour
{
    bool isPaused;

    public void OnMenu()
    {
        isPaused = !isPaused;
        transform.GetChild(0).gameObject.SetActive(isPaused);
        transform.GetChild(0).Find("newOptionsScreen").gameObject.SetActive(false);
    }
}

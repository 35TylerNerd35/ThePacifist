using UnityEngine;
using UnityEngine.UI;

public class CursorColour : MonoBehaviour
{
    void OnEnable()
    {
        float r = PlayerPrefs.GetInt("crosshairR", 255) / 255;
        float g = PlayerPrefs.GetInt("crosshairG", 255) / 255;
        float b = PlayerPrefs.GetInt("crosshairB", 255) / 255;

        GetComponent<Image>().color = new Color(r, g, b, 1);
    }
}

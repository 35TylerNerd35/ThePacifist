using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleOpacity : MonoBehaviour
{
    Image targetImg;

    void Awake()
    {
        targetImg = GetComponent<Image>();

        LoadSettingsData.SettingsUpdated += UpdateSettings;
        UpdateSettings();
    }

    public void UpdateSettings()
    {
        Color previousColor = targetImg.color;
        Color colour = new Color(previousColor.r, previousColor.g, previousColor.b, SaveSystem.data.subtitleOpacity / 255);
        targetImg.color = colour;
    }
}

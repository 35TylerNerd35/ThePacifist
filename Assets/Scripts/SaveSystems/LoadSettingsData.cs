using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSettingsData : MonoBehaviour
{
    public delegate void MyEvent();
    public static event MyEvent SettingsUpdated;

    [Header("Audio")]
    public Slider[] vols;
    public Toggle[] mutes;

    [Header("Gameplay")]
    public Slider[] sensSliders;
    public Slider fovVal;
    public Slider subtitleOpacity;
    public Toggle doHeadBob;
    public Toggle isFullscreen;

    void OnEnable()
    {
        for(int i = 0; i < vols.Length; i++)
        {
            vols[i].value = SaveSystem.data.vols[i];
        }

        for(int i = 0; i < mutes.Length; i++)
        {
            mutes[i].isOn = SaveSystem.data.mutes[i];
            mutes[i].GetComponent<ToggleGraphics>().ToggleValueChanged(mutes[i]);
        }

        for(int i = 0; i < sensSliders.Length; i++)
        {
            sensSliders[i].value = SaveSystem.data.sensSliders[i];
        }

        fovVal.value = SaveSystem.data.fovVal;
        subtitleOpacity.value = SaveSystem.data.subtitleOpacity;

        doHeadBob.isOn = SaveSystem.data.doHeadBob;
        doHeadBob.GetComponent<ToggleGraphics>().ToggleValueChanged(doHeadBob);

        isFullscreen.isOn = SaveSystem.data.isFullscreen;
        isFullscreen.GetComponent<ToggleGraphics>().ToggleValueChanged(isFullscreen);
    }

    void OnDisable()
    {
        for(int i = 0; i < vols.Length; i++)
        {
            SaveSystem.data.vols[i] = vols[i].value;
        }

        for(int i = 0; i < mutes.Length; i++)
        {
            SaveSystem.data.mutes[i] = mutes[i].isOn;
        }

        for(int i = 0; i < sensSliders.Length; i++)
        {
            SaveSystem.data.sensSliders[i] = sensSliders[i].value;
        }

        SaveSystem.data.fovVal = fovVal.value;
        SaveSystem.data.subtitleOpacity = subtitleOpacity.value;

        SaveSystem.data.doHeadBob = doHeadBob.isOn;
        SaveSystem.data.isFullscreen = isFullscreen.isOn;

        SaveSystem.SaveData();
        SettingsUpdated();
    }
}

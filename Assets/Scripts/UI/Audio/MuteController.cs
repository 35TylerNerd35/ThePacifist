using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MuteController : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    string channel;
    Toggle toggle;
    Slider slider;

    float targetVal;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        slider = transform.parent.GetComponentInChildren<Slider>();
        channel = transform.parent.name;

        toggle.onValueChanged.AddListener (delegate {ToggleChanged();});
    }

    void ToggleChanged()
    {
        targetVal = 0.001f;

        if(toggle.isOn)
        {
            targetVal = slider.value;
        }

        myMixer.SetFloat(channel, Mathf.Log10(targetVal) * 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    string channel;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        channel = transform.parent.name;

        slider.onValueChanged.AddListener (delegate {SliderChanged();});
    }

    void SliderChanged()
    {
        myMixer.SetFloat(channel, Mathf.Log10(slider.value) * 20);
    }
}

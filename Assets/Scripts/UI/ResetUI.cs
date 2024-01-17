using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetUI : MonoBehaviour
{
    public void ResetButton(Button var)
    {
        var.enabled = false;
        var.enabled = true;
    }

    public void ResetSlider(Slider var)
    {
        var.enabled = false;
        var.enabled = true;
    }

    public void ResetToggle(Toggle var)
    {
        var.enabled = false;
        var.enabled = true;
    }
}

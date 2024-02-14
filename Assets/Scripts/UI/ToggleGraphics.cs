using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGraphics : MonoBehaviour
{
    [SerializeField] GameObject toggleGraphic;
    [SerializeField] bool ivertGraphic = true;

    Toggle toggle;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
    }

    public void ToggleValueChanged(Toggle change)
    {
        if (change.isOn && !ivertGraphic)
            toggleGraphic.SetActive(true);

        else if(!change.isOn && ivertGraphic)
            toggleGraphic.SetActive(true);

        else
            toggleGraphic.SetActive(false);
    }
}

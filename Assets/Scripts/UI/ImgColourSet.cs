using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImgColourSet : MonoBehaviour
{
    [SerializeField] Image targetImg;

    void Awake() => SetColour();

    public void SetColour()
    {
        Color previousColor = targetImg.color;
        Color colour = new Color(previousColor.r, previousColor.g, previousColor.b, this.GetComponent<Slider>().value / 255);
        targetImg.color = colour;
    }
}

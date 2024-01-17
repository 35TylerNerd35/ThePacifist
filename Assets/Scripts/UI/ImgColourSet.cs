using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImgColourSet : MonoBehaviour
{
    [SerializeField] Image targetImg;

    public void SetColour()
    {
        Color colour = new Color(0, 0, 0, this.GetComponent<Slider>().value / 255);
        targetImg.color = colour;
    }
}

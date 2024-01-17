using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour
{
    [SerializeField] Transform picker;
    [SerializeField] float radius;

    bool _isPickerHeld;
    Vector3 initMousePos;
    Vector3 initPickerPos;

    void Awake()
    {
        initPickerPos = picker.localPosition;
    }

    public void PickerClick()
    {
        _isPickerHeld = !_isPickerHeld;
        initMousePos = Input.mousePosition;

        if(!_isPickerHeld)
        {
            PickColour();
        }
    }

    void Update()
    {
        if(_isPickerHeld)
        {
            //Find new pos based off mouse movement
            Vector3 newPos = (Input.mousePosition - initMousePos) / 5;

            //Change picker pos if it is within circle
            if(Vector3.Distance(picker.localPosition + newPos, initPickerPos) < radius)
            {
               picker.localPosition += newPos; 
            }
            
            //Set new pos
            initMousePos = Input.mousePosition;

            if(Input.GetMouseButtonDown(0))
            {
                _isPickerHeld = false;
                PickColour();
            }
        }
    }

    void PickColour()
    {

    }
}

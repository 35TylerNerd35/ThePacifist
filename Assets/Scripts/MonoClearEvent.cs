using UnityEngine;
using UnityEngine.EventSystems;

public class MonoClearEvent : MonoBehaviour
{
    public void Clear()
    {
        EventSystem.current.SetSelectedGameObject(null);  
    }
}

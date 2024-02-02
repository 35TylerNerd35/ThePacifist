using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;
    EventSystem eventSystem;

    void Awake()
    {
        anim = GetComponent<Animator>();
        eventSystem = EventSystem.current;

        // Attach a method to be called when the button is clicked
        GetComponent<Button>().onClick.AddListener(OnButtonPress);
    }

    public void OnButtonPress()
    {
        anim.Play("ButtonPressed");
        eventSystem.SetSelectedGameObject(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("isHovering", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("isHovering", false);
    }
}

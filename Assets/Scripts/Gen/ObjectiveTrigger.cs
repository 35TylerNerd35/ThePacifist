using UnityEngine;
using TMPro;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] Animator objAnim;
    [SerializeField] string newObj;
    [SerializeField] bool isDestroy;
    [SerializeField] bool isOnEnable;

    void OnEnable()
    {
        if(!isOnEnable)
            return;

        SetObj();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
            return;

        SetObj();
    }

    void SetObj()
    {
        //Format text element
        string initTxt = $"<alpha=#00>|><alpha=#FF> {newObj}";
        string visualTxt = $"<alpha=#FF>|><alpha=#00> {newObj}";

        //Anim text
        objAnim.transform.GetChild(0).GetComponent<TMP_Text>().text = initTxt;
        objAnim.transform.GetChild(1).GetComponent<TMP_Text>().text = visualTxt;
        objAnim.Play("ObjectiveIn");

        //Destroy object
        if(!isDestroy)
            return;

        Destroy(gameObject);
    }
}

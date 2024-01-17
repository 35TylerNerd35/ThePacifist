using UnityEngine;

public class VLPlayer : MonoBehaviour
{
    [SerializeField] subtitle[] subsToAdd;
    [Space]
    [SerializeField] bool _isOnTrigger;
    [SerializeField] bool _isOnEnable;

    [SerializeField] bool _doesClearPrevious;
    

    void Enable()
    {
        if(_isOnEnable)
        {
            PlayLine();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && _isOnTrigger)
        {
            PlayLine();
        }
    }

    void PlayLine()
    {
        VLManager manager = GameObject.FindWithTag("ManagerParent").transform.GetChild(2).GetComponent<VLManager>();

        if(_doesClearPrevious)
        {
            manager.ClearQueue();
        }

        //Add to queue
        for(int i = 0; i < subsToAdd.Length; i++)
        {
            manager.queued.Add(subsToAdd[i]);
        }
        
        //Ensure is playing
        manager.PlayAudio();

        Destroy(gameObject);
    }
}

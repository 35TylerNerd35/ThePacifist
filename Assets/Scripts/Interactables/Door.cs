using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] Transform door;
    [SerializeField] float doorMoveSpeed = 5f;
    [Space]
    [SerializeField] Vector3 closePos;
    [SerializeField] Vector3 openPos;
    [Space]
    [SerializeField] bool isMoving;
    [SerializeField] bool isOpen;
    [SerializeField] bool useLocal;
    [Space]
    [SerializeField] float openTime;

    public void Interaction()
    {
        if(!isMoving)
        {
            isMoving = true;

            StartCoroutine(CheckHasReachedState());
        }
    }

    void Update()
    {
        if(isMoving)
        {
            float moveStep = doorMoveSpeed * Time.deltaTime;

            if(isOpen)
            {
                if(useLocal)
                {
                    door.localPosition = Vector3.MoveTowards(door.localPosition, closePos, moveStep);
                }
                else
                {
                    door.position = Vector3.MoveTowards(door.position, closePos, moveStep);
                }
                
            }
            else
            {
                if(useLocal)
                {
                    door.localPosition = Vector3.MoveTowards(door.localPosition, openPos, moveStep);
                }
                else
                {
                    door.position = Vector3.MoveTowards(door.position, openPos, moveStep);
                }
            }
        }
    }

    IEnumerator CheckHasReachedState()
    {
        yield return new WaitForSeconds(openTime);
        isMoving = false;
        isOpen = !isOpen;
    }
}

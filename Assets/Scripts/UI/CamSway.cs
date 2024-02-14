using UnityEngine;

public class CamSway : MonoBehaviour
{
    [SerializeField] float horSwayAmount = 0.25f;
    [SerializeField] float horSwaySpeed = 1.0f;
    [SerializeField] float verticalSwayAmount = 0.1f; 
    [SerializeField] float verticalSwaySpeed = 1.5f; 

    Vector3 initPos;

    void Start()
    {
        initPos = transform.localPosition;
    }

    void Update()
    {
        // Calculate horizontal sway using a sine wave
        float horizontalSway = Mathf.Sin(Time.time * horSwaySpeed) * horSwayAmount;
        float verticalSway = Mathf.Sin(Time.time * verticalSwaySpeed) * verticalSwayAmount;

        // Combine horizontal and vertical sway
        Vector3 swayPosition = initPos + new Vector3(horizontalSway, verticalSway, 0);
        transform.localPosition = swayPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashingLights : MonoBehaviour
{
    [SerializeField] Light[] lights;
    [SerializeField] Vector2 R_waveDelay;
    [SerializeField] Vector2 R_newRange;
    [SerializeField] Vector2 R_newIntensity;

    void OnEnable()
    {
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        float waveDelay = Random.Range(R_waveDelay.x, R_waveDelay.y);
        yield return new WaitForSeconds(waveDelay);

        int light = Random.Range(0, lights.Length - 1);

        float newRange = Random.Range(R_newRange.x, R_newRange.y);
        float newIntensity = Random.Range(R_newIntensity.x, R_newIntensity.y);

        lights[light].range = newRange;
        lights[light].intensity = newIntensity;

        StartCoroutine(Flashing());
    }
}

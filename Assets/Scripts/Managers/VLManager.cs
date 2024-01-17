using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct speaker{
    public string name;
    public Color color;
}

[System.Serializable]
public struct subtitle{
    public string speaker;
    public string line;
    public AudioClip audio;
    public float delayBeforeNext;
}


public class VLManager : MonoBehaviour
{
    [SerializeField] float subDelayTime;
    [SerializeField] TMP_Text textObj;
    [SerializeField] GameObject textPanel;
    [SerializeField] AudioSource audioPlayer;

    [Header("Text Settings")]
    [SerializeField] speaker[] speakers;
    public List<subtitle> queued = new List<subtitle>(); 

    bool isPlayingAudio;
    string subColor;
    public void PlayAudio()
    {
        if(!isPlayingAudio)
        {
            isPlayingAudio = true;
            textPanel.SetActive(true);

            //Grab correct colour for name
            for(int i = 0; i < speakers.Length; i++)
            {
                if(speakers[i].name == queued[0].speaker)
                {
                    subColor = ColorUtility.ToHtmlStringRGB(speakers[i].color);
                }
            }

            //Set audio & subtitle
            audioPlayer.clip = queued[0].audio;
            audioPlayer.Play();
            textObj.text = $"<color=#{subColor}>{queued[0].speaker}</color>: {queued[0].line}";

            StartCoroutine(Timer());
        }
    }

    public void ClearQueue()
    {
        StopAllCoroutines();
        queued.Clear();

        audioPlayer.clip = null;
        textObj.text = "";
        textPanel.SetActive(false);



        isPlayingAudio = false;

        
    }

    IEnumerator Timer()
    {
        if(queued[0].audio != null)
        {
            //Wait for audio to finish
            yield return new WaitForSeconds(queued[0].audio.length);
        }
        else
        {
            Debug.Log($"<Color=Red>No audio clip found for line:</Color> '{queued[0].line}'");
            yield return new WaitForSeconds(2);
        }

        //Empty player when line ends
        audioPlayer.clip = null;
        yield return new WaitForSeconds(subDelayTime);

        //Empty text
        textObj.text = "";

        textPanel.SetActive(false);
        yield return new WaitForSeconds(queued[0].delayBeforeNext);
        textPanel.SetActive(true);

        isPlayingAudio = false;

        //Remove first queued element
        queued.RemoveAt(0);

        //Play next if more lines in queue
        if(queued.Count > 0)
        {
            PlayAudio();
        }
        else
        {
            textPanel.SetActive(false);
        }
    }
}

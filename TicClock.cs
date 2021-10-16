using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicClock : MonoBehaviour
{
    public Text timerText;

    public AudioClip tikSound;
    public AudioSource FxManagerAudioSource;

    private float nextUpdate = 1;

    private void Update()
    {
        if (nextUpdate > 0)
        {
            nextUpdate -= Time.deltaTime;
        }
        else
        {
            if (timerText.text == "00:00" || timerText.text == "02:35" || timerText.text == "02:34" || timerText.text == "02:33" || timerText.text == "02:32" || timerText.text == "02:31" || timerText.text == "05:00")
            {
                AudioSource.PlayClipAtPoint(tikSound, FindObjectOfType<PlayerLocalController>().myLocalPlayer.transform.position, FxManagerAudioSource.volume);
            }
            nextUpdate = 1;
        }
        
    }
}

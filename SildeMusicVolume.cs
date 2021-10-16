using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SildeMusicVolume : MonoBehaviour
{
    public Slider myself;
    public AudioSource actions;
    public AudioSource music;
    public AudioSource slideWind;
    private void Start()
    {
        VolumnSaver.volume = myself.value;
    }
    public void slideAction()
    {
        actions.volume = myself.value;
        VolumnSaver.volume = myself.value;
    }
    public void slideMusic()
    {
        music.volume = myself.value;
    }
    public void slidewind()
    {
        slideWind.volume = (myself.value * 2.8f) / 10;
    }
}

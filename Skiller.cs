using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Skiller : MonoBehaviour
{
    private float timeAlive;
    AudioSource adioSour;
    private void Start()
    {
        adioSour = gameObject.transform.Find("Audio Source").GetComponent<AudioSource>();
        adioSour.volume = VolumnSaver.volume;
        timeAlive = 8;
    }
    private void Update()
    {
        if (timeAlive >= 0)
        {
            timeAlive -= Time.deltaTime;
        }
        else
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}

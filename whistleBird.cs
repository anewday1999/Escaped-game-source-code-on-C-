using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class whistleBird : MonoBehaviour
{
    public AudioClip[] _audio;
    [PunRPC]
    public void whistle(int stt, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(_audio[stt], pos, GetComponent<AudioSource>().volume);
    }
}

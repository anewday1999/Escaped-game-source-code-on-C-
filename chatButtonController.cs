using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class chatButtonController : MonoBehaviour
{
    public GameObject scrollView;
    public Text countPlayers;
    public void turnOnOffChat()
    {
        if (PhotonNetwork.InRoom)
        {
            countPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount + " in room.";
        }
        scrollView.SetActive(!scrollView.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class settingManager : MonoBehaviour
{
    public Camera standbyCamera;
    public Button settingButton;
    public GameObject settingPannel;

    public void pannelOpenClose()
    {
        settingPannel.SetActive(!settingPannel.activeSelf);
    }
    public void closePannle()
    {
        settingPannel.SetActive(false);
    }
    public void callBackToLobby()
    {
        if (PhotonNetwork.InRoom)
        {
            standbyCamera.enabled = true;
            PhotonNetwork.LeaveRoom();
        }
        if (settingPannel != null)
        {
            settingPannel.gameObject.SetActive(false);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UiListPlayerController : MonoBehaviour
{
    public GameObject[] listPlayerInLocal;
    public Player[] listPlayer;
    public List<string> listOption;
    public Dropdown drDown;

    private void Awake()
    {
        listOption = new List<string>(21);
    }
    public void getListPlayer()
    {
        listPlayer = PhotonNetwork.PlayerList;
    }
    private void OnEnable()
    {
        getListPlayer();
        setSlots();
        string s = PhotonNetwork.LocalPlayer.NickName;

        for (int i = 0; i < drDown.options.Count; i++)
        {
            if (drDown.options[i].text == s)
            {
                drDown.value = i;
                Debug.Log(i);
            }
        }
        for (int i = 0; i < listPlayerInLocal.Length; i++)
        {
            //Debug.Log(listPlayerInLocal[i].name);
            //Debug.Log(listPlayerInLocal[i].GetPhotonView().owner.NickName + " " + s);
            if (listPlayerInLocal[i] != null)
            {
                if (listPlayerInLocal[i].GetPhotonView().Owner.NickName == s)
                {

                    FindObjectOfType<PlayerLocalController>().playLocalUserCamera.target = listPlayerInLocal[i].transform;
                }
            }
            
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < listPlayerInLocal.Length; i++)
        {
            //Debug.Log(listPlayerInLocal[i].name);
            //Debug.Log(listPlayerInLocal[i].GetPhotonView().owner.NickName + " " + s);
            if (listPlayerInLocal[i] != null)
            {
                if (listPlayerInLocal[i].GetPhotonView().Owner.NickName == PhotonNetwork.LocalPlayer.NickName)
                {
                    FindObjectOfType<PlayerLocalController>().playLocalUserCamera.target = listPlayerInLocal[i].transform;
                }
            }
        }
    }
    public void onClickOption()
    {
        string s = drDown.options[drDown.value].text;
        for (int i = 0; i < listPlayerInLocal.Length; i++)
        {
            //Debug.Log(listPlayerInLocal[i].name);
            //Debug.Log(listPlayerInLocal[i].GetPhotonView().owner.NickName + " " + s);
            if (listPlayerInLocal[i] != null)
            {
                if (listPlayerInLocal[i].GetPhotonView().Owner.NickName == s)
                {

                    FindObjectOfType<PlayerLocalController>().playLocalUserCamera.target = listPlayerInLocal[i].transform;
                }
            }
            
        }
    }
    public void setSlots()
    {
        if (PhotonNetwork.InRoom)
        {
            if (listOption != null)
            {
                listOption.Clear();
            }

            drDown.ClearOptions();
            if (listPlayer != null)
            {
                foreach (Player p in listPlayer)
                {
                    listOption.Add(p.NickName);
                }
            }
            drDown.AddOptions(listOption);
        }
        
    }
}

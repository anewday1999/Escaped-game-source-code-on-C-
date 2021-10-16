using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StartButtonControl : MonoBehaviour
{
    public AdmobManager adMobManager;
    public Button startButton;
    
    IEnumerator waitForSet(float second)
    {
        callLoadingStart();
        callsetseam();
        yield return new WaitForSeconds(second);
        setlocalTeam();
        callrespawn();
        callSetAppearance();
        destroyOnClick();
        callSetCameraToMe();
        requestAds();
        callResetScale();
        callSetActiveFire();
    }
    public void onClickStart()
    {
        StartCoroutine(waitForSet(4));
    }
    public void callSetActiveFire()
    {
        FindObjectOfType<MatchManager>().GetComponent<PhotonView>().RPC("setFireButton", Photon.Pun.RpcTarget.All);
    }
    void callLoadingStart()
    {
        FindObjectOfType<MatchManager>().GetComponent<PhotonView>().RPC("callActiveLoadStart", Photon.Pun.RpcTarget.All);
    }
    public void requestAds() //////////////**********
    {
        
        adMobManager.GetComponent<PhotonView>().RPC("callLoad", Photon.Pun.RpcTarget.All);
        
    }
    public void callsetseam()      //////////////**********
    {
        FindObjectOfType<MatchManager>().setTeam();
    }
    public void setlocalTeam() //////////////**********
    {
        FindObjectOfType<MatchManager>().GetComponent<PhotonView>().RPC("setLocalTeam", Photon.Pun.RpcTarget.All);
    }
    public void callrespawn() //////////////**********
    {
        FindObjectOfType<MatchManager>().callResPawnTrue();
    }
    public void callSetAppearance() //////////////**********
    {
        FindObjectOfType<AppearanceManager>().GetComponent<PhotonView>().RPC("setAppearance", Photon.Pun.RpcTarget.All);
    }
    public void destroyOnClick() //////////////**********
    {
        //FindObjectOfType<PlayerLocalController>().changeActive(false);
    }
    public void callSetCameraToMe() //////////////**********
    {
        FindObjectOfType<PlayerLocalController>().GetComponent<PhotonView>().RPC("setCameraToMyself", Photon.Pun.RpcTarget.All);
    }
    public void callResetScale() //////////////**********
    {
        FindObjectOfType<PlayerLocalController>().GetComponent<PhotonView>().RPC("resetScale", Photon.Pun.RpcTarget.All);
    }
    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
                {
                    startButton.interactable = true;
                }
                else
                {
                    startButton.interactable = false;
                }
            }
            
        }
        
    }
}

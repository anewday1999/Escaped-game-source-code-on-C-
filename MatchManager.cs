using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class MatchManager : MonoBehaviour
{
    public UnityEngine.UI.Button fireButton;
    public UnityEngine.UI.Image loadingStart;
    Vector3 target;
    Vector3 realPosition;
    Quaternion realRotation;

    int index;

    public bool Playing;

    public playerShooting playerShootLocal;

    public GameObject LockAndUnlock;

    public SpawnSpot tarSpawnspot;

    public SpawnSpot[] spawnSpots1;
    public SpawnSpot[] spawnSpots2;


    public bool isRespawn;


    private void Start()
    {
        Playing = false;
    }
    [PunRPC]
    void setRespawnTrue()
    {
        findSpot();
        isRespawn = true;
    }
    public void callResPawnTrue()
    {
        GetComponent<PhotonView>().RPC("setRespawnTrue", Photon.Pun.RpcTarget.All);
    }
    void findSpot()
    {
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 1)
        {
            tarSpawnspot = spawnSpots1[Random.Range(0, spawnSpots1.Length)];
        }
        else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2)
        {
            tarSpawnspot = spawnSpots2[Random.Range(0, spawnSpots2.Length)];
        }
        else
        {
            tarSpawnspot = spawnSpots2[Random.Range(0, spawnSpots2.Length)];
        }
    }
    [PunRPC]
    public void setFireButton()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["code"] != null)
        {
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2)
            {
                fireButton.gameObject.SetActive(false);
            }
            else
            {
                fireButton.gameObject.SetActive(true);
            }
        }
    }
    IEnumerator waitingAndSet(float second)
    {
        yield return new WaitForSeconds(second);
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if ((int)p.CustomProperties["code"] != 1)
            {
                p.SetCustomProperties(TeamStaticVar.team2);
                Debug.Log("inset:" + p.NickName + " " + TeamStaticVar.team2.Values.ToString());
            }
        }
    }
    public void setTeam() //for masterClient
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            p.SetCustomProperties(TeamStaticVar.team0);
        }
        /*for (int i = 0; i < s.Length; i++)
        {
            Debug.Log(s[i].NickName + " " + s[i].GetTeam());
        } */
        for (int j = 0; j < (int) (PhotonNetwork.PlayerList.Length / 5) + 1; j++)
        {
            index = Random.Range(0, PhotonNetwork.PlayerList.Length);
            while ((int)PhotonNetwork.PlayerList[index].CustomProperties["code"] == 1)
            { 
                index = Random.Range(0, PhotonNetwork.PlayerList.Length);
            }
            PhotonNetwork.PlayerList[index].SetCustomProperties(TeamStaticVar.team1);
            Debug.Log("inset:" + PhotonNetwork.PlayerList[index].NickName + " " + TeamStaticVar.team1.Values.ToString());

        }
        /*for (int i = 0; i < s.Length; i++)
        {
            Debug.Log(s[i].NickName + " " + s[i].GetTeam());
        } */
        StartCoroutine(waitingAndSet(2));
    }
    [PunRPC]
    public void callActiveLoadStart()
    {
        loadingStart.gameObject.SetActive(true);
    }
    [PunRPC]
    public void setLocalTeam()
    {
        FindObjectOfType<ClockManager>().byPlayer = 0;
        FindObjectOfType<ClockManager>().currentTime = 330;
        Playing = true;

        if((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2)
        {
            FindObjectOfType<PlayerLocalController>().namelable.gameObject.SetActive(false);
            FindObjectOfType<PlayerLocalController>().changeSpeed(8.5f);
            FindObjectOfType<PlayerLocalController>().changeActive(true);
            playerShootLocal.enabled = false;
            LockAndUnlock.SetActive(true);
            LockAndUnlock.transform.Find("Lock").gameObject.SetActive(true);
            LockAndUnlock.transform.Find("Unlock").gameObject.SetActive(false);
            FindObjectOfType<PlayerLocalController>().playLocalUserCamera.maxDistance = 40;
        }
        else
        {
            FindObjectOfType<PlayerLocalController>().changeSpeed(8.5f);
            playerShootLocal.enabled = true;
            LockAndUnlock.SetActive(false);
            FindObjectOfType<PlayerLocalController>().playLocalUserCamera.maxDistance = 20;
        }
    }
}
